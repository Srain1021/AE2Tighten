using System;
using Dapper.Contrib.Extensions;

namespace AE2Tightening
{
    [Table("TopEngineResult")]
    public class TopEngineResultModel
    {
        [Key]
        public int Id { get; set; }

        public string EngineCode { get; set; }

        public string StationId { get; set; }

        public int? Result { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
