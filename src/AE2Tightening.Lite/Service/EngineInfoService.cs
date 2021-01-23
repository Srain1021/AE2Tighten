using AE2Tightening.Lite;
using FreeSql.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AE2Tightening.Lite
{
    public class EngineInfoService
    {

        public async Task<int> Insert(EngineInfoModel model)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                model.Id = (int)(await FreeHanlder.SqliteHandler.Insert<EngineInfoModel>()
                       .AppendData(model)
                       .ExecuteIdentityAsync());
                return model.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EngineInfoModel> Get(string code)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                List<EngineInfoModel> lsteg = await FreeHanlder.SqliteHandler.Select<EngineInfoModel>()
                    .Where(e => e.EngineCode == code)
                    .Limit(1)
                    .ToListAsync();
                return lsteg.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EngineInfoModel>> GetAllAsync(int page,int pageSize)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                return await FreeHanlder.SqliteHandler.Select<EngineInfoModel>()
                    .Page(page,pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Update(EngineInfoModel model)
        {
            if(model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                return await FreeHanlder.SqliteHandler.Update<EngineInfoModel>()
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
