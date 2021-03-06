﻿using System.Windows.Forms;
using System.Xml.Serialization;

namespace AE2Tightening.Configura
{
    [XmlRoot("Config")]
    public class XmlParamter
    {
        [XmlElement("BarCodePattern")]
        public string BarCodePattern { get; set; }

        [XmlElement("Rfid")]
        public RfidConfig Rfid { get; set; }

        [XmlElement("Adam")]
        public AdamConfig Adam { get; set; }

        public SerialConfig Scanner { get; set; }

        [XmlElement("Screenlist")]
        public ScreenConfig[] Screenlist { get; set; }
        public int DisplanQueueNum { get; set; }
        public string NullCode { get; set; }
    }
}
