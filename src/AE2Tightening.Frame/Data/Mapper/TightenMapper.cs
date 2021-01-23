using AE2Devices;
using AE2Tightening.Lite;
using AE2Tightening.Models;
using System;

namespace AE2Tightening.Frame.Data
{
    public class TightenMapper
    {
        public static TighteningResultModel ServerMap(TightenData data)
        {
            return new TighteningResultModel
            {
                EngineCode = data.EngineCode,
                DataNO = data.Pset,
                BoltNO = data.BoltNo,
                Torque = data.Torque,
                Angle = data.Angle,
                Result = data.Result,
                ResultTime = data.TightenTime,
                CreateTime = DateTime.Now
            };
        }

        public static TightenModel LocalMap(TightenData data)
        {
            return new TightenModel
            {
                EngineCode = data.EngineCode,
                Pset = data.Pset,
                BoltNo = data.BoltNo,
                Torque = data.Torque,
                Angle = data.Angle,
                Result = data.Result,
                CreateTime = data.TightenTime
            };
        }

        public static TighteningResultModel MapTightenData(TightenModel data)
        {
            return new TighteningResultModel
            {
                StationID = data.StationName,
                EngineCode = data.EngineCode,
                DataNO = data.Pset,
                BoltNO = data.BoltNo,
                Torque = data.Torque,
                Angle = data.Angle,
                Result = data.Result,
                ResultTime = data.CreateTime,
                CreateTime = DateTime.Now
            };
        }
    }
}
