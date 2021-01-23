using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;

namespace AE2Tightening
{
    [Table(Name = "TConfig")]
    public class TConfigModel
    {
        [Column(IsPrimary =true,IsIdentity = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
