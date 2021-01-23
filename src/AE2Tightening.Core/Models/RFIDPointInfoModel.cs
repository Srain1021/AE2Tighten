using Dapper.Contrib.Extensions;

namespace AE2Tightening.Models
{
    [Table("RFIDPointInfo")]
    public class RFIDPointInfoModel
    {
        [Key]
        public int ID { get; set; }

        public string EngineCode { get; set; }

        public string DeviceId { get; set; }

        public string VNo { get; set; }
    }
}
