using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
  //  [Table("LEngineQueue")]
   public class EngineQueueModel
    {
        [Key]
        public int TNo { get; set; }
        public string EngineCode { get; set; }
        public string EngineMTO { get; set; }
        public string EngineType { get; set; }
        public string TCaseType { get; set; }
        public string AutoType { get; set; }
        public int RepairStatus { get; set; }
        public string PNo { get; set; }

    }
}
