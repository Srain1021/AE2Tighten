using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper.Contrib.Extensions;

namespace AE2Tightening
{
    [Table("TopLineEngineQueue")]
    public class TopLineEngineQueueModel
    {
        [Key]
        public int TNo { get; set; }
        public string EngineCode { get; set; }
        public string EngineMTO { get; set; }
        public string EngineType { get; set; }
        public string TCaseType { get; set; }
    }
}
