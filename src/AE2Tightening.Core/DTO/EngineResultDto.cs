using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE2Tightening.Models
{
    public class EngineResultDto
    {

        public int TID { get; set; }

        public string EngineCode { get; set; }

        public int Result { get; set; }

        public DateTime? EndTime { get; set; }

        public string ResultField { get; set; }

        public string EndTimeField { get; set; }
    }
}
