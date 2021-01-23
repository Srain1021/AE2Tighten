using System;
using FreeSql.DataAnnotations;

namespace AE2Tightening.Lite
{
    [Table(Name = "T_EngineInfo")]
    public class EngineInfoModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        public string StationName { get; set; }

        public string EngineCode { get; set; }

        public string EngineType { get; set; }

        public string PartCode { get; set; }

        public int Result { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
