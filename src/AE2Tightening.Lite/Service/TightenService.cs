using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeSql;

namespace AE2Tightening.Lite
{
    public class TightenService
    {
        private string table = "T_TightenData";

        /// <summary>
        /// 保存拧紧数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(TightenModel model)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                model.Id = (int)FreeHanlder.SqliteHandler.Insert<TightenModel>()
                    .AppendData(model)
                    .ExecuteIdentity();
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }

        public async Task<TightenModel> Get(string station,string engineCode)
        {
            if (FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            List<TightenModel> tds = await FreeHanlder.SqliteHandler.Select<TightenModel>()
                .Where(t => t.StationName == station && t.EngineCode == engineCode)
                .Limit(1)
                .ToListAsync();
            return tds.FirstOrDefault();
        }

        public async Task<long> GetCount(string code,DateTime sTime,DateTime endTime)
        {
            if (FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            //List<TightenModel> tds =
            ISelect<TightenModel> query = FreeHanlder.SqliteHandler.Select<TightenModel>()
              .Where(t => t.EngineCode.Contains(code));
            if (sTime != null)
                query = query.Where(t => t.CreateTime > sTime);
            if (endTime != null)
                query = query.Where(t => t.CreateTime < endTime);
            return await query.CountAsync();
        }

        public async Task<List<TightenModel>> GetTightensAsync(string code, DateTime sTime, DateTime endTime,int page,int pagesize)
        {
            if (FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            ISelect<TightenModel> query = FreeHanlder.SqliteHandler.Select<TightenModel>()
              .Where(t => t.EngineCode.Contains(code));
            if (sTime != null)
                query = query.Where(t => t.CreateTime > sTime);
            if (endTime != null)
                query = query.Where(t => t.CreateTime < endTime);
            return await query.Page(page, pagesize).NoTracking().ToListAsync();
        }

        public async Task<List<TightenModel>> GetNoUploadDataAsync(int count)
        {
            if (FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            return await FreeHanlder.SqliteHandler.Select<TightenModel>()
                .Where(t => t.IsUpload != 1)
                .Limit(count)
                .NoTracking()
                .ToListAsync();
        }

        public async Task<int> UpdateAsync(TightenModel model)
        {
            if (FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            return await FreeHanlder.SqliteHandler.Update<TightenModel>()
                .SetSource(model)
                .ExecuteAffrowsAsync();
        }
    }
}
