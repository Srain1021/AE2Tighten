using AE2Tightening.Lite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Lite.Service
{
    /// <summary>
    /// 本地拧紧结果表
    /// </summary>
    public class LTopEngineResultService
    {

        public async Task<int> InsertAsync(LTopEngineResultModel model)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                model.Id = (int)(await FreeHanlder.SqliteHandler.Insert<LTopEngineResultModel>()
                       .AppendData(model)
                       .ExecuteIdentityAsync());
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LTopEngineResultModel> GetAsync(string engineCode)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                List<LTopEngineResultModel> lsteg = await FreeHanlder.SqliteHandler.Select<LTopEngineResultModel>()
                    .Where(e => e.EngineCode == engineCode)
                    .Limit(1)
                    .ToListAsync();
                return lsteg.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LTopEngineResultModel>> GetNoUploadDataAsync(int count)
        {
            if (FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            return await FreeHanlder.SqliteHandler.Select<LTopEngineResultModel>()
                .Where(t => t.IsUpload != 1)
                .Limit(count)
                .NoTracking()
                .ToListAsync();
        }


        public async Task<int> UpdateAsync(LTopEngineResultModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                return await FreeHanlder.SqliteHandler.Update<LTopEngineResultModel>()
                    .SetSource(model)
                    .ExecuteAffrowsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
