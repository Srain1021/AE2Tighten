using AE2Tightening.Configura;
using AE2Tightening.Frame.Configura.Configs.Tighten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Configura
{
    public class TightenConfig
    {
        public bool CodeRequest { get; set; }

        public EnumCodeType CodeType { get; set; }

        public SocketConfig Socket { get; set; }

        //public TdPoint[] Points { get; set; }
        public int BoltCount { get; set; }

        public SQLFields SQLFields { get; set; }
    }
}
