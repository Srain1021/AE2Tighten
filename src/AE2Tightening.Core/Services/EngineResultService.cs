using AE2Tightening.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;
using AE2Tightening.Core;

namespace AE2Tightening.Services
{
    public class EngineResultService: ServiceBase
    {
        string tb = "LEngineResult";

        public EngineResultDto Get(string code,string resultField,string endTimeField)
        {
           return EngineResultMapper.Map(Get(code), resultField, endTimeField);
        }

        public EngineResultModel Get(string code)
        {
            if (code == null)
                return null;

            return this.Invoke((c) =>
            {
                return c?.QueryFirstOrDefault<EngineResultModel>($"select top 1 * from {tb} where EngineCode=@EngineCode", new { EngineCode = code });
            });
        }

        public bool Insert(EngineResultModel model)
        {
            if (model == null) throw new System.ArgumentNullException(nameof(model));

            return this.Invoke((c) =>
            {
                return c?.Insert(model) > 0;
            });
        }

        public bool Update(EngineResultModel model)
        {
            if (model == null) throw new System.ArgumentNullException(nameof(model));

            return this.Invoke((c) =>
            {
                return c.Update(model);
            });
        }

        public int Update(EngineResultDto dto)
        {
            if (dto == null || dto.TID < 1) throw new System.ArgumentNullException(nameof(dto));
            return this.Invoke(c => {
                return c.Execute($"Update LEngineResult set {dto.ResultField}=@Result,{dto.EndTimeField}=@EndTime where TID={dto.TID}", dto);
            });
        }

        public bool Update(EngineResultModel model,string ResultFiled,string TimeField)
        {
            if (model == null) throw new System.ArgumentNullException(nameof(model));
            return this.Invoke(c=> {
                return c.Execute($"Update LEngineResult set {ResultFiled}=@TightenResult,{TimeField}=@TightenedTime where TID={model.TID}", model) > 0;
            });
        }
    }
}
