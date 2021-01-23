using System;
using System.IO.Ports;
using AE2Tightening.Configura;

namespace AE2Devices
{
    public class SerialScanController : IScanController
    {
        public SerialConfig Config { get; }

        public bool IsOpen { get; private set; }

        public Action<string> OnScanCoded { get; set; }
        private SerialPort serialPort = null;

        public SerialScanController(SerialConfig config)
        {
            Config = config;
        }
        public void Close()
        {
            if(serialPort != null)
            {
                serialPort.Close();
                IsOpen = false;
            }
        }

        public bool Open()
        {
            if(serialPort == null)
            {
                serialPort = new SerialPort
                {
                    PortName = Config.PortName,
                    BaudRate=  Config.BuadRate,
                    DataBits = Config.DataBits,
                    StopBits = StopBits.One,
                    Parity = Parity.None
                };
                serialPort.DataReceived += SerialPort_DataReceived;
            }
            serialPort.Open();
            IsOpen = serialPort.IsOpen;
            return serialPort.IsOpen;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = serialPort.ReadLine();
            OnScanCoded?.Invoke(data);
        }
    }
}
