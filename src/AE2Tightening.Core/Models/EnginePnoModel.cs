using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
    [Table("Match_BarPNO")]
   public class EnginePnoModel
    {
        [Key]
        public int TID { get; set; }
        public string StationID { get; set; }
        public string BarFeatureCode { get; set; }
        public string BarFeatureIndex { get; set; }
        public string PNO { get; set; }
        public int State { get; set; }
        public string Bak { get; set; }
    }
}
