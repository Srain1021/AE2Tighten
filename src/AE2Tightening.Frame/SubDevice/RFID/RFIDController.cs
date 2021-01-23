using AE2Tightening.Configura;
using GodSharp.Balluff.Commands;
using GodSharp.Balluff.Commands.Host;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GodSharp.Logging.Abstractions;

namespace AE2Tightening.Frame
{
    public class RFIDController : IRFIDController
    {
        public Action<string> OnRfidReaded { get; set; }
        public Action<bool> OnNetChanged { get; set; }

        private ICmdHost host = null;
        private readonly RfidConfig rfid;
        private readonly Logging _log;
        private Regex regex = null;
        private string lastEngineCode = null;
        
        public RFIDController(RfidConfig config,string codePattern, Logging log)
        {
            _log = log;
            rfid = config;
            regex = new Regex(codePattern);
        }

        /// <summary>
        /// 异步启动RFID
        /// </summary>
        public void Start()
        {
            if (rfid.Available)
            {
                try
                {
                    CmdHostFactory factory = new CmdHostFactory();
                    host = factory.CreateTcpClientHost(
                            new TcpClientOptions(rfid.Socket.Host, rfid.Socket.Port)
                            {
                                OnDisconnected = ()=> { 
                                    _log.Warn("rfid断开连接。");
                                    OnNetChanged?.Invoke(false);
                                },
                                OnTryConnecting = (i) => _log.Info("rfid正在重连。"),
                                OnConnected = ()=> { 
                                    _log.Info("rfid已连接。");
                                    OnNetChanged?.Invoke(true);
                                }
                            },
                            new CmdClientOptions(rfid.ProtocolType)
                            {
                                AutoOutputType = rfid.OutputType,
                                AutoOutput = rfid.OutputOptions,
                                OutputHandler = (b, t) => RfidOutPutHandler(b, t)
                            }
                        );
                    Task.Run(() => OpenRFID(host));
                }
                catch (Exception ex)
                {
                    _log.Error("RFID初始化异常", ex);
                }
            }
        }
      
        private void OpenRFID(ICmdHost _host)
        {
            try
            {
                bool state = _host.Start();
            }
            catch (Exception ex)
            {
                _log.Error("RFID启动失败", ex);
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
                string readCode = Encoding.ASCII.GetString(buffer, 0, 12)
                    .Replace("\r", "")//去除特殊字符
                    .Replace("\n", "")
                    .Replace("\0", "")
                    .Replace("\t", "");

                if (string.IsNullOrEmpty(readCode))
                {
                    _log.Warn($"RFID读取到空标签！");
                    return;
                }
                if (!regex.IsMatch(readCode))
                {
                    _log.Warn($"条码格式不符：{readCode}");
                    return;
                }
                if (lastEngineCode != null && lastEngineCode == readCode)
                {
                    _log.Warn($"条码重复读取：{readCode}");
                    return;
                }
                _log.Info($"RFID读取到发动机条码：{readCode}");
                OnRfidReaded?.Invoke(readCode);
            }
            catch(Exception ex)
            {
                _log.Error("RFID读取标签出现异常", ex);
            }
        }

        public void CloseAsync()
        {
            host.Stop();
        }

        public string GetReadCode()
        {
            return lastEngineCode;
        }
     
        public void Test()
        {
            string code = "L15B51000155";
            byte[] buffer = Encoding.ASCII.GetBytes(code);
            RfidOutPutHandler(buffer, AutoOutputType.None);
        }
       
    }
}
