namespace AE2Tightening.Configura
{
    public class ScreenConfig
    {
        public int Id { get; set; }

        public string Title { get; set; }

        //public OpcConfig[] Opcs { get; set; }

        //public SerialConfig Scanner { get; set; }

        public StationInfo Station { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TightenConfig[] Tighten { get; set; }

        public PartConfig Part { get; set; }
    }
}
