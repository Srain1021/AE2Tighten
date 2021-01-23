using Dapper.Contrib.Extensions;
using System;


namespace AE2Tightening.Models
{
    [Table("Match_EngineCode")]
    public class EngineAutoTypeModel
    {
        [Key]
        public int TID { get; set; }
        public string DeriveFeatureCode { get; set; }
        public string DeriveFeatureIndex { get; set; }
        public string TCaseType { get; set; }
        public string FeatureCode { get; set; }
        public string FeatureIndex { get; set; }
        public int State { get; set; }
        public string Bak { get; set; }

    }
}
