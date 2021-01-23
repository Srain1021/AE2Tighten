using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE2Tightening.Configura;
using ToolsAPI.Interface;

namespace AE2Devices
{
    public class AE2DeviceFactory
    {
        public IRFIDController CreateRFIDDevice(RfidConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("RfidConfig参数不能为null");
            IRFIDController rfid = new RFIDController(config);
            return rfid;
        }

        public IAdamController CreateAdamDevice(int serialPort)
        {
            if (serialPort < 1)
                throw new ArgumentException("Adam参数的值不是正确的端口号");
            IAdamController adam = new AdamController(serialPort);
            return adam;
        }

        public IScanController CreateScanDevice(SerialConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("SerialConfig参数不能为null");
            IScanController scan = new SerialScanController(config);
            return scan;
        }


        public IOpcController CreateOpcDevice(OpcConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("OpcConfig参数不能为null");
            IOpcController opc = new OPCSocketServer(config);
            return opc;
        }

        public ICardController CreateCardDevice(MwCardConfig config)
        {
            if(config == null)
                throw new ArgumentNullException("Card PortConfig参数不能为null");
            ICardController card = new MwCardController(config);
            return card;
        }

        public ITightenController CreateTightenDevice(TightenConfig config)
        {
            if (config == null)
                throw new ArgumentNullException("TightenConfig参数不能为null");
            ITightenController tighten = new TightenController(config);
            return tighten;
        }
    }
}
