using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
    [Table("LSubFrameResult")]
   public class SubFrameModel
    {
        [Key]
        public int TID { get; set; }
        public string EngineCode { get; set; }
        public string AutoType { get; set; }
        public string SubFrameCode { get; set; }
        public string PNo { get; set; }
        public int SubFrameFinalResult { get; set; }
        public DateTime? SubFrameTime { get; set; }
    }
}
