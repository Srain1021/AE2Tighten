using Dapper.Contrib.Extensions;

namespace AE2Tightening.Models
{
    [Table("Match_EngineType")]
    public class EngineTypeModel
    {
        [Key]
        public int TID { get; set; }
        public string TypeName { get; set; }
        public string FeatureCode { get; set; }
        public string FeatureIndex { get; set; }
        public string Bak { get; set; }
        public int State { get; set; }
    }
}
