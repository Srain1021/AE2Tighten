using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using AE2Tightening.Configura;
using Serilog;

namespace AE2Devices
{
    internal class OPCSocketServer : IOpcController
    {
        public Action<bool> PassChangedAction { get; set; }
        public Action<bool> ShieldChangedAction { get; set; }

        public bool NetStatus { get; set; }

        public Action<IDevice, bool> NetChangedAction { get; set; }
        private Socket skt = null;
        private OpcConfig config;
        private System.Timers.Timer heartTimer = new System.Timers.Timer();
        private DateTime lastHeart = DateTime.Now;
        public OPCSocketServer(OpcConfig cfg)
        {
            config = cfg;
            heartTimer.Interval = 5000;
            heartTimer.Elapsed += HeartTimer_Elapsed;
        }

        private void HeartTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(skt != null && NetStatus)
            {
                try
                {
                    if (lastHeart.AddSeconds(8) < DateTime.Now)
                    {
                        NetStatus = false;
                        Task.Run(() => Open());
                    }
                    else
                    {
                        SendMessage("KeepAlive,1");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "OPC服务通讯心跳报错");
                }
            }
        }

        public void Close()
        {
            if(skt != null && NetStatus)
            {
                skt.Close();
            }
            skt = null;
        }

        private bool SendMessage(string str)
        {
            try
            {
                if (skt.Send(Encoding.UTF8.GetBytes(str)) > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "发送消息到OPC服务失败，{str}",str);
            }
            return false;
        }

        public bool GetShieldValue()
        {
            OpcItem item = config.Items.FirstOrDefault(x => x.Misc == "ReadShieldSystem");
            SendMessage($"GET,{item.ItemId}");
            return true;
        }

        public bool Open()
        {
            try
            {
                if (skt != null && NetStatus)
                {
                    return true;
                }
                skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.BeginConnect("192.168.8.200", 10010, ConnectCallBack, skt);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "连接OPC服务报错");
            }
            return false;
        }
    
        private void ConnectCallBack(IAsyncResult e)
        {
            if (e.IsCompleted)
            {
                skt.EndConnect(e);
                NetStatus = true;
                NetChangedAction?.Invoke(this, true);
                lastHeart = DateTime.Now;
                Thread td = new Thread(new ThreadStart(Receive));
                td.IsBackground = true;
                td.Start();
            }
        }

        private void Receive()
        {
            while (true)
            {
                if(skt != null && NetStatus)
                {
                    try
                    {
                        byte[] buff = new byte[200];
                        int len = skt.Receive(buff);
                        if (len == 0)
                        {
                            NetStatus = false;
                            NetChangedAction?.Invoke(this, false);
                            Task.Run(() => Reconnect());
                            return;
                        }
                        string str = Encoding.UTF8.GetString(buff, 0, len);
                        string[] arr = str.Split(',');
                        if(arr[0] == "OPC")
                        {
                            if (arr.Length > 2)
                            {
                                OpcItem item = config.Items.FirstOrDefault(x => x.ItemId == arr[1]);
                                if (item != null)
                                {
                                    switch (item.Misc)
                                    {
                                        case "Pass":
                                            {
                                                bool rst = Convert.ToBoolean(arr[2]);
                                                PassChangedAction?.Invoke(rst);
                                                break;
                                            }
                                        case "ReadShieldSystem":
                                            {
                                                bool rst = Convert.ToBoolean(arr[2]);
                                                ShieldChangedAction?.Invoke(rst);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        else if(arr[0] == "KeepAlive")
                        {
                            lastHeart = DateTime.Now;
                        }
                    }
                    catch (SocketException ex)
                    {
                        NetStatus = false;
                        NetChangedAction?.Invoke(this, false);
                        Task.Run(() => Reconnect());
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private void Reconnect()
        {
            if (skt == null)
                return;
            Thread.Sleep(5000);
            Open();
        }
        public void OpenAsync()
        {
            
        }

        public bool Pass()
        {
            OpcItem item = config.Items.FirstOrDefault(x => x.Misc == "Pass");
            if (item != null)
            {
               return SendMessage($"SET,{item.ItemId},1");
            }
            return false;   
        }
    }
}
