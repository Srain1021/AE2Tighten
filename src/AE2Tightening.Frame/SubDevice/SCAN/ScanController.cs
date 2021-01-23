using AE2Tightening.Configura;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodSharp.SerialPort;
using GodSharp.Logging.Abstractions;
using System.Text.RegularExpressions;

namespace AE2Tightening.Frame
{
    class ScanController : IScanController
    {
        public event OnScanCodeDelegate OnScanCoded;
        private GodSerialPort port;
        private Logging _logger;
        private Regex codeRegex = new Regex("^[0-9].");
        public SerialConfig Config { get; }

        public bool IsOpen { get; private set; }

        public ScanController(SerialConfig config, Logging logger)
        {
            _logger = logger;
            Config = config;
            port = new GodSerialPort(c =>
            {
                c.PortName = Config.PortName;
                c.BaudRate = Config.BuadRate;
                c.DataBits = Config.DataBits;
                c.StopBits = StopBits.One;
                c.Parity = Parity.None;
            });
            port.OnData = OnDataRead;
        }

        private void OnDataRead(GodSerialPort port, byte[] data)
        {
            string code = Encoding.ASCII.GetString(data);
            if(code.Length > 10)
            {
                if (codeRegex.IsMatch(code))
                {
                    OnScanCoded?.Invoke(code,EnumCodeType.PartCode);
                }
                else
                {
                    OnScanCoded?.Invoke(code, EnumCodeType.EngineCode);
                }
                _logger.Info($"接收到扫描枪数据：{code}");
            }
        }
       

        public void Close()
        {
            port.Close();
        }

        public bool Open()
        {
            try
            {
                IsOpen = port.Open();
                _logger.Info($"扫描枪连接{(IsOpen ? "成功" : "失败")}.");
                return IsOpen;
            }
            catch (Exception ex)
            {
                _logger.Error("扫描器连接异常", ex);
                IsOpen = false;
                return false;
            }
          
        }
    }
}
