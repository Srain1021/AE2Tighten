using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
    [Table("Match_DerivePNO")]
   public class DerivePnoModel
    {
        [Key]
        public int TID { get; set; }
        public string StationID { get; set; }
        public string DeriveFeatureCode { get; set; }
        public string DeriveFeatureIndex { get; set; }
        public string PNO { get; set; }

        public string BarFeatureIndex { get; set; }
        public int State { get; set; }
        public string Bak { get; set; }
    }
}
