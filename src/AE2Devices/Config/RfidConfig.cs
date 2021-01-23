using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GodSharp.Balluff;
using GodSharp.Balluff.Commands;

namespace AE2Devices
{
    public class RfidConfig
    {
        public string Name { get; set; }

        public string DeviceId { get; set; }

        public ProtocolType ProtocolType { get; set; }

        public AutoOutputType OutputType { get; set; }

        public AutoOutputOptions OutputOptions { get; set; }

        public string Host { get; set; }

        public int RemotePort { get; set; }

        public bool Available { get; set; }

        public string Misc { get; set; }
    }
}
