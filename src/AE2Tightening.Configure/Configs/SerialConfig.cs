namespace AE2Tightening.Configura
{
    public class SerialConfig
    {
        public string PortName { get; set; }
        
        public int BuadRate { get; set; }
        
        public int DataBits { get; set; }
        
        public string Parity { get; set; }
        
        public string StopBits { get; set; }
        
        public string Misc { get; set; }
        
        public bool Available { get; set; }
    }
}