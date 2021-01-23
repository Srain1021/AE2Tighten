using AE2Tightening.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace AE2Tightening.Services
{     
    public class EngineTypeService : ServiceBase
    {
        public EngineTypeModel Get(string code)
        {
            if (code == null) throw new System.ArgumentNullException(nameof(code));

            return this.Invoke((c) =>
            {
                //IDbConnection.QueryFirstOrDefault<EngineTypeModel>() 即是Dapper框架的SqlMapper类提供的方法.
                return c.QueryFirstOrDefault<EngineTypeModel>("select * from Match_EngineType where FeatureCode=@FeatureCode", new { FeatureCode = code });
            });
        }

        public List<EngineTypeModel> GetAll()
        {
            return this.Invoke((c) =>
            {
                return c.Query<EngineTypeModel>("select * from Match_EngineType where State=1")?.ToList();
            });
        }
    }
}
