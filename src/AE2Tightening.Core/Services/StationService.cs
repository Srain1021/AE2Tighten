using AE2Tightening.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AE2Tightening.Services
{
    public class StationService : ServiceBase
    {
        public StationModel Get(string stationid)
        {
            if (stationid == null) throw new System.ArgumentNullException(nameof(stationid));

            return this.Invoke((c) =>
            {
                return c.QueryFirstOrDefault<StationModel>("select top 1 * from StationInfo where StationID=@StationID", new { StationID = stationid });
            });
        }


        public bool Update(StationModel model)
        {
            if (model == null) throw new System.ArgumentNullException(nameof(model));

            return this.Invoke((c) =>
            {
                return c.Update(model);
            });
        }
    }
}
