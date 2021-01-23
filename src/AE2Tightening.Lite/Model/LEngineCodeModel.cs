using System;
using FreeSql.DataAnnotations;

namespace AE2Tightening.Lite
{
    [Table(Name = "LEngineCode")]
    public class LEngineCodeModel
    {
        [Column(IsPrimary =true,IsIdentity =true)]
        public int Id { get; set; }
        public string DeriveFeatureCode { get; set; }
        public string DeriveFeatureIndex { get; set; }
        public string TCaseType { get; set; }
        public string FeatureCode { get; set; }
        public string FeatureIndex { get; set; }
    }
}
