using GodSharp.Balluff.Commands;
using GodSharp.Balluff.Commands.Host;
using System;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using AE2Tightening.Configura;

namespace AE2Devices
{
    internal class RFIDController : IRFIDController
    {
        public Action<string> OnRfidReaded { get; set; }
        public Action<IDevice,bool> NetChangedAction { get; set; }

        public bool NetStatus { get; set; }
        public Action<TagData> OnTagDataReaded { get; set; }

        private ICmdHost host = null;
        private readonly RfidConfig rfid;
        private string lastEngineCode = null;
        
        public RFIDController(RfidConfig config)
        {
            rfid = config;
        }
        /// <summary>
        /// 创建通讯实例
        /// </summary>
        private void CreateHost()
        {
            CmdHostFactory factory = new CmdHostFactory();
            host = factory.CreateTcpClientHost(
                    new TcpClientOptions(rfid.Host, rfid.RemotePort)
                    {
                        OnDisconnected = () => {
                            Log.Warning("rfid断开连接。");
                            NetStatus = false;
                            NetChangedAction?.Invoke(this,false);
                        },
                        OnTryConnecting = (i) => Log.Information("rfid正在重连。"),
                        OnConnected = () => {
                            Log.Information("rfid已连接。");
                            NetStatus = true;
                            NetChangedAction?.Invoke(this, true);
                        }
                    },
                    new CmdClientOptions(rfid.ProtocolType)
                    {
                        AutoOutputType = rfid.OutputType,
                        AutoOutput = rfid.OutputOptions,
                        OutputHandler = (b, t) => RfidOutPutHandler(b, t)
                    }
                );
        }
        /// <summary>
        /// 异步启动RFID
        /// </summary>
        public async void OpenAsync()
        {
            if (rfid.Available)
            {
                try
                {
                    CreateHost();
                    await Task.Run(() => OpenRFID(host));
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "RFID初始化异常");
                }
            }
        }
        /// <summary>
        /// 同步连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            CreateHost();
            return OpenRFID(host);
        }
      
        private bool OpenRFID(ICmdHost _host)
        {
            try
            {
                return _host.Start();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "RFID启动失败");
                return false;
            }
        }

        /// <summary>
        /// 读标签响应函数
        /// </summary>
        /// <param name="buffer">标签数据？</param>
        /// <param name="type"></param>
        private void RfidOutPutHandler(byte[] buffer, AutoOutputType type)
        {
            try
            {
                //取得标签条码
                string readCode = Encoding.ASCII.GetString(buffer, 0, buffer.Length)
                    .Replace("\r", "")//去除特殊字符
                    .Replace("\n", "")
                    .Replace("\0", "")
                    .Replace("\t", "");
                if (string.IsNullOrEmpty(readCode))
                {
                    Log.Warning("RFID读取到空标签！");
                }
                else
                {
                    lastEngineCode = readCode;
                }
                //Log.Information("{Name} RFID读取到发动机条码：{readCode}", rfid.Name,readCode);
                OnRfidReaded?.Invoke(readCode);
                var tag = new TagData(readCode);
                OnTagDataReaded?.Invoke(tag);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "RFID读取标签出现异常");
            }
        }

        public void Close()
        {
            host.Stop();
        }

        public string GetReadCode()
        {
            return lastEngineCode;
        }
     
    }
}
