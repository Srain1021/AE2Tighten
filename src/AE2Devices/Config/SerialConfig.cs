namespace AE2Devices
{
    public class SerialConfig
    {
        public string PortName { get; set; }
        
        public int BuadRate { get; set; }
        
        public int DataBits { get; set; }
        
        public string Parity { get; set; }
        
        public int StopBits { get; set; }
        
        public bool Available { get; set; }
    }
}