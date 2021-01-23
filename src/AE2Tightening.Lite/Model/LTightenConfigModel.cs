using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace AE2Tightening.Lite
{
    [Table(Name = "LTightenConfig")]
    public class LTightenConfigModel
    {
        [Column(IsPrimary =true,IsIdentity =true)]
        public int Id { get; set; }

        public string FeatureCode { get; set; }

        public string FeatureIndex { get; set; }

        public string StationId { get; set; }

        public int TightenPointNum { get; set; }

        public int ToolId { get; set; }

        public int? JobId { get; set; }

        public int? BoltPset1 { get; set; }
        public int? BoltPset2 { get; set; }
        public int? BoltPset3 { get; set; }
        public int? BoltPset4 { get; set; }
        public int? BoltPset5 { get; set; }
        public int? BoltPset6 { get; set; }
        public int? BoltPset7 { get; set; }
        public int? BoltPset8 { get; set; }
        public int? BoltPset9 { get; set; }
        public int? BoltPset10 { get; set; }
    }
}
