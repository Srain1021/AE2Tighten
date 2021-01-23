using System;
using System.IO.Ports;
using System.Text;
using AE2Tightening.Configura;
using GodSharp.SerialPort;
using Serilog;

namespace AE2Devices
{
    internal class ScanController : IScanController
    {
        public Action<string> OnScanCoded { get; set; }
        private GodSerialPort port;
        public SerialConfig Config { get; }

        public bool IsOpen { get; private set; }

        public ScanController(SerialConfig config)
        {
            Config = config;
            port = new GodSerialPort(c =>
            {
                c.PortName = Config.PortName;
                c.BaudRate = Config.BuadRate;
                c.DataBits = Config.DataBits;
                c.StopBits = StopBits.One;
                c.Parity = Parity.None;
            })
            {
                OnData = OnDataRead
            };
        }

        private void OnDataRead(GodSerialPort port, byte[] data)
        {
            try
            {
                string code = Encoding.ASCII.GetString(data);
                if (code.Length > 10)
                {
                    OnScanCoded?.Invoke(code);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "扫描枪接收数据时异常。");
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
                Log.Information("扫描枪连接{IsOpen}.", IsOpen ? "成功" : "失败");
                return IsOpen;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "扫描器连接异常");
                IsOpen = false;
                return false;
            }
          
        }
    }
}
