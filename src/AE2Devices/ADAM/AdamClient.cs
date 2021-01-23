using Advantech.Adam;
using Advantech.Common;

namespace AE2Devices
{
    internal class AdamClient
    {
        private int address;
        private AdamCom adamCom;
        private Adam4000Config config;

        public int SerialPortNumber { get; private set; }
        public int Address => address;
        public int DiNumber { get; private set; }
        public int DoNumber { get; private set; }
        public Adam4000Type AdamType { get; private set; }
        public Adam4000Config ModuleConfig => config;

        public bool Opened { get; set; }
        public bool Checksum { get; private set; } = false;

        public AdamClient()
        {
            address = 1;
        }

        public AdamClient(Adam4000Type type, int port, int address = 1, bool checksum = false) : this()
        {
            Initialize(type, port, address, checksum);
        }

        public void Initialize(Adam4000Type type, int port, int address = 1, bool checksum = false)
        {
            this.AdamType = type;
            this.SerialPortNumber = port;
            this.address = address;
            this.Checksum = checksum;

            this.DiNumber = DigitalInput.GetChannelTotal(type);
            this.DoNumber = DigitalOutput.GetChannelTotal(type);
        }

        public void Open()
        {
            adamCom = new AdamCom(SerialPortNumber);
            adamCom.Checksum = Checksum;

            bool opened = false;
            try
            {
                if (!adamCom.IsOpen) opened = adamCom.OpenComPort();

                if (!opened) return;

                // set COM port state, 9600,N,8,1
                adamCom.SetComPortState(Baudrate.Baud_9600, Databits.Eight, Parity.None, Stopbits.One, FlowControl.None);
                // set COM port timeout
                adamCom.SetComPortTimeout(500, 1000, 0, 1000, 0);

                if (!adamCom.Configuration(address).GetModuleConfig(out config))
                {
                    opened = false;
                    adamCom.CloseComPort();
                    return;
                }
            }
            finally
            {
                Opened = opened;
            }
        }

        public bool Get(out bool[] di, out bool[] @do) => adamCom.DigitalInput(address).GetValues(DiNumber, DoNumber, out di, out @do);

        public bool Set(int channel, bool val)
        {
            if (DoNumber > 8) // ADAM-4056S, ADAM-4056SO
                return adamCom.DigitalOutput(address).SetSValue(channel, val);
            else
                return adamCom.DigitalOutput(address).SetValue(channel, val);
        }

        public bool Set(bool[] vals)
        {
            return adamCom.DigitalOutput(address).SetValues(vals);
        }

        public void Close() => Opened = adamCom.IsOpen ? !adamCom.CloseComPort() : false;
    }

}
