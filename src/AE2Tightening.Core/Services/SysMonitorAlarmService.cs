using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AE2Tightening.Models;
using Dapper.Contrib.Extensions;

namespace AE2Tightening.Services
{
    public class SysMonitorAlarmService : ServiceBase
    {
        public bool Insert(SysMonitorAlarmModel model)
        {
            return this.Invoke((c) =>
            {
                return c?.Insert(model) > 0;
            });
        }
    }
}
