using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
    [Table("SysMonitorAlarm")]
    public class EngineWarnModel
    {
        [Key]
        public int TID { get; set; }
        public string AlarmType { get; set; }
        public string StationID { get; set; }
        public string StationName { get; set; }
        public string AlarmContent { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
