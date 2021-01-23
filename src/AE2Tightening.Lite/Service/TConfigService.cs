using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Lite
{
    public class TConfigService
    {
        public TConfigModel GetValue(string name)
        {
            if (FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            return FreeHanlder.SqliteHandler.Select<TConfigModel>()
                .Where(t => t.Name == name)
                .ToOne();
        }

        public Task<int> UpdateAsync(TConfigModel model)
        {
            if (FreeHanlder.SqliteHandler == null)
                throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
            return FreeHanlder.SqliteHandler.InsertOrUpdate<TConfigModel>().SetSource(model).ExecuteAffrowsAsync();
        }
    }
}
