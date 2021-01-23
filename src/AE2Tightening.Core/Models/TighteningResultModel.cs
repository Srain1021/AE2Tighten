using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE2Tightening.Models
{
    [Table("TopTighteningResult")]
    public class TighteningResultModel
    {
        [Key]
        public int TID { get; set; }
        public string EngineCode { get; set; }
        public string StationID { get; set; }
        //public string StationName { get; set; }
        public int DataNO { get; set; }
        //public int Pset { get; set; }
        public int BoltNO { get; set; }
        public double Torque { get; set; }
        public decimal Angle { get; set; }
        public int Result { get; set; }
        public DateTime? ResultTime { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
