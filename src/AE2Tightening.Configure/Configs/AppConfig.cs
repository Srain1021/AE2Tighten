using System.Xml.Serialization;

namespace AE2Tightening.Configura
{
    [XmlRoot("Config")]
    public class AppConfig
    {
        [XmlElement("BarCodePattern")]
        public string BarCodePattern { get; set; }

        [XmlElement("Rfid")]
        public RfidConfig Rfid { get; set; }

        [XmlElement("Adam")]
        public AdamConfig Adam { get; set; }

        public MwCardConfig MwCard { get; set; }

        public SerialConfig Scanner { get; set; }

        public OpcConfig Opc { get; set; }

        [XmlElement("Screenlist")]
        public ScreenConfig[] Screenlist { get; set; }
        public int DisplanQueueNum { get; set; }
        public string NullCode { get; set; }

        public string[] IgnoreEngine { get; set; }
    }
}
