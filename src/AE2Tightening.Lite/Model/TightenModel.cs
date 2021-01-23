using FreeSql.DataAnnotations;
using System;

namespace AE2Tightening.Lite
{
    /// <summary>
    /// 拧紧数据
    /// 螺丝级
    /// </summary>
    [Table(Name = "T_TightenData")]
    public class TightenModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        public string StationName { get; set; }

        public string EngineCode { get; set; }

        public int Pset { get; set; }

        public int BoltNo { get; set; }

        /// <summary>
        /// 力矩
        /// </summary>
        public double Torque { get; set; }

        public decimal Angle { get; set; }

        public int Result { get; set; }

        public int IsUpload { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
