using FreeSql.DataAnnotations;
using System;

namespace AE2Tightening.Lite.Model
{
    /// <summary>
    /// 本地拧紧结果
    /// 工位级
    /// </summary>
    [Table(Name = "LTopEngineResult")]
    public class LTopEngineResultModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        public string EngineCode { get; set; }

        public string StationId { get; set; }

        public int Result { get; set; }

        public int brand { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int IsUpload { get; set; }

    }
}
