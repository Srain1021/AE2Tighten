using GodSharp.Opc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using System.Threading;
using Serilog;
using AE2Tightening.Configura;

namespace AE2Devices
{
    internal class OpcController : IOpcController
    {
        private readonly OpcDaClient opcClient;
        private Dictionary<string, OpcItem> dicItems = null;
        private DateTime heartTime = DateTime.Now;
        private System.Timers.Timer timer = new System.Timers.Timer();//心跳定时器
        private bool IsPLCShield = false;//是否屏蔽(PLC)
        public bool NetStatus { get; set; }
        private bool LineStatus { get; set; }//线条状态
        private OpcConfig Config { get; }
        public Action<bool> PassChangedAction { get; set; }

        public Action<IDevice,bool> NetChangedAction { get; set; }

        public Action<bool> ShieldChangedAction { get; set; }

        public OpcController(OpcConfig config)
        {
            timer.Interval = 5 * 1000;
            timer.Elapsed += Timer_Elapsed;
            Config = config;
            opcClient = new OpcDaClient(x =>
            {
                x.ProgId = config.ProgId;
                x.Tags = config.Items.Select(xx => new OpcTagItem() { Id = xx.Id, ItemId = xx.ItemId, Misc = xx.Misc }).ToArray();
                x.DefaultGroupUpdateRate = 100;
            })
            {
                Shutdown = (s, client) =>
                {
                    if (NetStatus)
                    {
                        NetStatus = false;
                        NetChangedAction?.Invoke(this, false);
                    }
                    Log.Warning("OPC Shutdown,{s}", s);
                },
                DataChange = OpcDataChanged
            };
        }

        /// <summary>
        /// opc数据响应
        /// </summary>
        /// <param name="para"></param>
        /// <param name="items"></param>
        private void OpcDataChanged(DataChangeParameter para,IEnumerable<OpcTagItem> items)
        {
            if (items != null && items.Count() > 0)
                Task.Run(() => ProcessOpcData(items));
        }
        /// <summary>
        /// opc消息处理
        /// </summary>
        /// <param name="items"></param>
        private void ProcessOpcData(IEnumerable<OpcTagItem> items)
        {
            foreach (var item in items)
            {
                if (item == null)
                    continue;
                try
                {
                    if (item.Value == null)
                        continue;
                    bool value = false;
                    switch (item.Misc)
                    {
                        case "Heartbeat":
                            value = Convert.ToBoolean(item.Value);
                            if(!value)
                            {
                                heartTime = DateTime.Now;
                                if (NetStatus == false)
                                {
                                    NetStatus = true;
                                    NetChangedAction?.Invoke(this, true);
                                }
                            }
                            break;
                        case "ReadShieldSystem":
                            IsPLCShield = Convert.ToBoolean(item.Value);
                            ShieldChangedAction?.Invoke(IsPLCShield);
                            break;
                        case "NoPass"://这里可以做信号校验
                            LineStatus = Convert.ToBoolean(item.Value);
                            PassChangedAction?.Invoke(LineStatus);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "读取opc节点[{Misc}]信息异常",item.Misc);
                }
            }
        }

        public void Close()
        {
            timer?.Stop();
            if(opcClient != null && opcClient.Connected)
            {
                NetStatus = false;
                opcClient.Disconnect();
            }
        }

        public bool Open()
        {
            if (opcClient == null)
            {
                throw new Exception("OPC客户端程序没有初始化.");
            }
            try
            {
                dicItems = Config.Items.ToDictionary(x => x.Misc?.ToString(), x => x);
                opcClient.Connent();
                Log.Information("OPC已连接");
                NetStatus = opcClient.Connected;
                heartTime = DateTime.Now;
                NetChangedAction?.BeginInvoke(this, true, null, null);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "OPC通讯连接异常");
                return false;
            }
            finally
            {
                if (dicItems.ContainsKey("Heartbeat"))
                {
                    timer.Start();
                }
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public async void OpenAsync()
        {
            try
            {
                if (opcClient?.Connected == false)
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            opcClient.Connent();
                            Log.Information("OPC已异步连接");
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "OPC通讯连接异常");
                        }
                    });
                    NetStatus = opcClient.Connected;
                    NetChangedAction?.Invoke(this,opcClient.Connected);
                    dicItems = Config.Items.ToDictionary(x => x.Misc?.ToString(), x => x);
                    if (dicItems.ContainsKey("Heartbeat"))
                    {
                        timer.Start();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "opc启动异常");
            }
        }

        /// <summary>
        /// 停线
        /// </summary>
        public bool Pass()
        {
            string p = "Pass";
            if (WriteBeforeCheck(p))
            {
                bool result = opcClient.AsycnWriter(dicItems[p].ItemId, 1);
                Log.Information("写入{point}地址值{value}{result}", p, 1, result ? "成功" : "失败");
                return result;
            }
            else
            {
                return false;
            }
        }

       
        /// <summary>
        /// 心跳定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if ((DateTime.Now - heartTime).TotalSeconds > 15)
                {
                    NetStatus = false;
                    NetChangedAction?.Invoke(this, false);
                    heartTime = DateTime.Now;
                }
                if (opcClient != null && opcClient.Connected)
                    Heartbeat();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "OPC心跳程序异常");
            }
        }

        /// <summary>
        /// 心跳
        /// </summary>
        private void Heartbeat()
        {
            if(WriteBeforeCheck("Heartbeat"))
            {
                opcClient.AsycnWriter(dicItems["Heartbeat"].ItemId, 1);
            }
        }

      

        /// <summary>
        /// 写值前的状态检查
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool WriteBeforeCheck(string item)
        {
            if (opcClient == null)
            {
                Log.Warning("写OPC[{item}]节点时，opcClient为null。",item);
                return false;
            }
            else if (dicItems == null || !dicItems.ContainsKey(item))
            {
                Log.Warning("没有找到[{item}]节点，无法写值。", item);
                return false;
            }
            return true;
        }

       
        public bool GetShieldValue()
        {
            if (!WriteBeforeCheck("ReadShieldSystem"))
            {
                return false;
            }
            OpcTagItem item = opcClient.SyncRead(dicItems["ReadShieldSystem"].ItemId);
            if (item != null)
            {
                return Convert.ToBoolean(item.Value);
            }
            else
            {
                return false;
            }
        }
    }
}
