using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AE2Tightening.Services;
using Dapper;
using Dapper.Contrib.Extensions;

namespace AE2Tightening
{
    
    public class TopLineEngineQueueService : ServiceBase
    {

        public TopLineEngineQueueModel Get(string code)
        {
            return Invoke(c => c.QueryFirstOrDefault<TopLineEngineQueueModel>("select * from TopLineEngineQueue where EngineCode = @code",new { code},commandTimeout:3));
        }
    }
}
