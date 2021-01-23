using AE2Tightening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE2Tightening.Core
{
    public class EngineResultMapper
    {
        //public static EngineResultModel Map(EngineResultDto dto)
        //{
        //    if (dto == null)
        //        return null;
        //    var model =  new EngineResultModel();
        //    typeof(EngineRepairModel).GetField(dto.ResultField).SetValue(model, dto.Result);
        //    typeof(EngineRepairModel).GetField(dto.EndTimeField).SetValue(model, dto.EndTime);
        //    model.TID = dto.TID;
        //    model.EngineCode = dto.EngineCode;
        //    return model;
        //}


        public static EngineResultDto Map(EngineResultModel result,string resultField,string endTimeField)
        {
            if (result == null)
                return null;
            var dto = new EngineResultDto
            {
                TID = result.TID,
                EngineCode = result.EngineCode,
                ResultField = resultField,
                EndTimeField = endTimeField
            };
            object dtoresult = typeof(EngineResultModel).GetProperty(resultField).GetValue(result,null);
            if(dtoresult != null)
            {
                if (int.TryParse(dtoresult.ToString(), out int r))
                    dto.Result = r;
            }
            object obj = typeof(EngineResultModel).GetProperty(endTimeField).GetValue(result,null);
            if (obj != null)
            {
                if (DateTime.TryParse(obj.ToString(), out DateTime dt))
                {
                    dto.EndTime = dt;
                }
            }
            return dto;
        }
    }
}
