using System;
using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace AE2Tightening.Models
{
    [Table("SysMonitorAlarm")]
    public class SysMonitorAlarmModel
    {
        [Key]
        public int TID { get; set; }

        public string AlarmType { get; set; }

        public string StationID { get; set; }

        public string StationName { get; set; }

        public string AlarmContent { get; set; }
    }
}
