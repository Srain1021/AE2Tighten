using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AE2Tightening.Models;
using Dapper;

namespace AE2Tightening.Services
{
    public class EngineInfoService : ServiceBase
    {
        public EngineInfoModel Get(string code)
        {
            if (code == null)
                return null;

            return this.Invoke((c) =>
            {
                return c?.QueryFirstOrDefault<EngineInfoModel>("select top 1 TNo,EngineCode,EngineMTO,EngineType,TCaseType from EngineInfo where EngineCode=@EngineCode", new { EngineCode = code });
            });
        }
    }
}
