namespace AE2Tightening.Configura
{
    public class OpcConfig
    {
        public bool Available { get; set; }
        public int Key { get; set; }
        public string Misc { get; set; }
        public OpcServer Server { get; set; }
        
        public OpcItem[] Items { get; set; }

    }

    public enum ResetState
    {
        None=0,
        WithoutStart = 1,
        WithoutStop = 2,
        WithException = 3

    }
}
