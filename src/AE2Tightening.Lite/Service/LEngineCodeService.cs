using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Lite
{
    public class LEngineCodeService
    {
        public bool InsertList(List<LEngineCodeModel> model)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                int len = FreeHanlder.SqliteHandler.Insert<LEngineCodeModel>()
                       .AppendData(model)
                       .ExecuteAffrows();
                return len == model.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LEngineCodeModel Get(string mto)
        {
            try
            {
                if (FreeHanlder.SqliteHandler == null)
                    throw new NullReferenceException("未初始化SqliteHandler数据库访问程序;");
                List<LEngineCodeModel> lsteg = FreeHanlder.SqliteHandler.Select<LEngineCodeModel>().ToList();
                return lsteg.FirstOrDefault(x => mto.GetString(x.DeriveFeatureIndex.Split(',').Select(xx => int.Parse(xx) - 1).ToArray())
                        .Equals(x.DeriveFeatureCode, StringComparison.CurrentCultureIgnoreCase));
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
                FreeHanlder.SqliteHandler.Delete<LEngineCodeModel>().Where(t => t.Id > 0).ExecuteAffrows();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
