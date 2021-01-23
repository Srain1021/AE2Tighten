using AE2Tightening.Models;
using Dapper.Contrib.Extensions;

namespace AE2Tightening.Services
{
    public class RFIDPointInfoService : ServiceBase
    {
        public bool Insert(RFIDPointInfoModel model)
        {
            return this.Invoke(c =>
            {
                return c.Insert(model) > 0;
            });
        }
    }
}
