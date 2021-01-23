using System;
using FreeSql.DataAnnotations;

namespace AE2Tightening.Lite
{
    [Table(Name = "LEngineType")]
    public class LEngineTypeModel
    {
        [Column(IsIdentity =true,IsPrimary =true)]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string FeatureCode { get; set; }
        public string FeatureIndex { get; set; }

    }
}
