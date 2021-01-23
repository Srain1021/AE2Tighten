using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AE2Tightening.Lite
{
    public class LTightenConfigService
    {
        public async Task<List<LTightenConfigModel>> GetTdConfig(string code,string stationId)
        {
            if(FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            List<LTightenConfigModel> lstCfg = await FreeHanlder.SqliteHandler.Select<LTightenConfigModel>()
                .Where(t => t.StationId == stationId)
                .ToListAsync();
            return lstCfg.FindAll(t=> code.GetString(t.FeatureIndex.Split(',').Select(xx => int.Parse(xx) - 1).ToArray())
                        .Equals(t.FeatureCode, StringComparison.CurrentCultureIgnoreCase));
        }

        public bool InsertList(List<LTightenConfigModel> model)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                int len = FreeHanlder.SqliteHandler.Insert<LTightenConfigModel>()
                       .AppendData(model)
                       .ExecuteAffrows();
                return len == model.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteAll()
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                return FreeHanlder.SqliteHandler.Delete<LTightenConfigModel>().Where(t => t.Id > 0).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
