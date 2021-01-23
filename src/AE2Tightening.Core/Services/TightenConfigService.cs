using AE2Tightening.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE2Tightening.Services
{
    public class TightenConfigService : ServiceBase
    {
        //public TightenConfig Get(string stationId)
        //{
        //    return this.Invoke(C =>
        //    {
        //        return C.QueryFirstOrDefault<TightenConfig>("select * from TightenConfig where StationId = @stationId", new { stationId });
        //    });
        //}

        public List<NewTightenConfig> GetNew(string Station)
        {
            List<NewTightenConfig> lstConfig = Invoke(c =>
            {
                return c.Query<NewTightenConfig>("select * from NewTightenConfig where StationId = @Station", new { Station }).ToList();
            });
            return lstConfig;
        }

        /// <summary>
        /// 根据拧紧枪序号和工位(工序)查找当前抢号需要拧紧的螺丝数
        /// </summary>
        /// <param name="Station"></param>
        /// <param name="toolNum"></param>
        /// <returns></returns>
        public List<NewTightenConfig> GetNewTightenConfigsByStationId(string Station, int? toolNum = null)
        {
            List<NewTightenConfig> lstConfig = Invoke(c =>
            {

                return c.Query<NewTightenConfig>("select * from NewTightenConfig where StationId = @Station and ToolId = @toolNum",
                    new { Station, toolNum }).ToList();
            });

            System.Diagnostics.Debug.WriteLine($"SQL->GetNewTightenConfigsByStationId -> {lstConfig?[0].ToolId}");

            return lstConfig;

            // return lstConfig.Where(c => engineCode.GetString(c.EngineFeatureIndex.Split(',').Select(i => int.Parse(i) - 1).ToArray()).Equals(c.EngineFeatureCode)).ToList();
        }

        /// <summary>
        /// 根据拧紧枪序号和工位(工序)查找当前抢号需要拧紧的螺丝数
        /// 备用，缓存找不到数据时 单独调用此方法
        /// </summary>
        /// <param name="Station"></param>
        /// <param name="toolNum"></param>
        /// <returns></returns>
        public List<NewTightenConfig> GetNewTightenConfigsByStationId(string Station, string engineCode, int? toolNum = null)
        {
            List<NewTightenConfig> lstConfig = Invoke(c =>
            {

                return c.Query<NewTightenConfig>("select * from NewTightenConfig where StationId = @Station and ToolId = @toolNum",
                    new { Station, toolNum }).ToList();
            });

            System.Diagnostics.Debug.WriteLine($"SQL->GetNewTightenConfigsByStationId -> {lstConfig?[0].ToolId}");


            return lstConfig.Where(c => engineCode.GetString(c.EngineFeatureIndex.Split(',').Select(i => int.Parse(i) - 1).ToArray()).Equals(c.EngineFeatureCode)).ToList();
        }

    }
}
