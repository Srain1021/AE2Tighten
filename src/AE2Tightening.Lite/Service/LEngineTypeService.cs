using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Lite
{
    public class LEngineTypeService
    {
        public bool InsertList(List<LEngineTypeModel> model)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                int len = FreeHanlder.SqliteHandler.Insert<LEngineTypeModel>()
                       .AppendData(model)
                       .ExecuteAffrows();
                return len == model.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LEngineTypeModel Get(string code)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                List<LEngineTypeModel> lsteg = FreeHanlder.SqliteHandler.Select<LEngineTypeModel>().ToList();
                return lsteg.FirstOrDefault(x => code.GetString(x.FeatureIndex.Split(',').Select(xx => int.Parse(xx) - 1).ToArray())
                        .Equals(x.FeatureCode, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteAll()
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                FreeHanlder.SqliteHandler.Delete<LEngineTypeModel>().Where(t => t.Id > 0).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
