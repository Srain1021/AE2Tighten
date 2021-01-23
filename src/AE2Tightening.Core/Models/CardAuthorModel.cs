using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace AE2Tightening.Models
{
    [Table("CardAuthor")]
    public class CardAuthorModel
    {
        [Key]
        public int Id { get; set; }

        public string CardNo { get; set; }

        public string Name { get; set; }

        public int RunLine { get; set; }

        public int SystemShield { get; set; }
    }
}
