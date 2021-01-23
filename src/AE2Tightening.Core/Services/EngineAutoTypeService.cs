using AE2Tightening.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AE2Tightening.Services
{
   public class EngineAutoTypeService:ServiceBase
    {
        public List<EngineAutoTypeModel> GetAll()
        {
            return this.Invoke((c) =>
            {
                return c?.Query<EngineAutoTypeModel>("select * from Match_EngineCode where State=1")?.ToList();
            });
        }


    }
}
