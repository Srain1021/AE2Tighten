using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE2Tightening.Services;
using Dapper.Contrib.Extensions;

namespace AE2Tightening
{
    public class TopEngineResultService : ServiceBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public long Insert(TopEngineResultModel result)
        {
            if (result == null)
                return 0;
            return Invoke(c => c.Insert(result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Update(TopEngineResultModel result)
        {
            return Invoke(c => c.Update(result));
        }
    }
}
