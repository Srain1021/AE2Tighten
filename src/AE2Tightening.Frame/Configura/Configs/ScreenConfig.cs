using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AE2Tightening.Configura
{
    public class ScreenConfig
    {
        public int Id { get; set; }

        public string Title { get; set; }

        //[XmlElement("Opcs")]
        public OpcConfig[] Opcs { get; set; }

        //[XmlElement("Scanner")]
        //public SerialConfig Scanner { get; set; }

        //[XmlElement("Station")]
        public StationInfo Station { get; set; }

        //[XmlElement("Tighten")]
        public TightenConfig Tighten { get; set; }
        //[XmlElement("Part")]
        public PartConfig Part { get; set; }
    }
}
