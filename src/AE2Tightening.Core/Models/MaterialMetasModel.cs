using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
    [Table("MaterialMetas")]
    public class MaterialMetasModel
    {
        [Key]
        public int Id { get; set; }

        public int ResultId { get; set; }

        public string EngineCode { get; set; }

        public string StationCode { get; set; }

        public string MaterialCode { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public int? Result { get; set; }
    }
}
