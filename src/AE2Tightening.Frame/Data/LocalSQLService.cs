using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE2Tightening.Lite;
using AE2Tightening.Lite.Model;
using AE2Tightening.Lite.Service;

namespace AE2Tightening.Frame.Data
{
    /// <summary>
    /// 本地Sqlite数据库
    /// </summary>
    public class LocalSQLService
    {
        public EngineInfoService EngineService { get;  }

        public TightenService TighteningService { get;  }

        public TConfigService TConfigTable { get; }

        public LEngineTypeService LEngineType { get; }

        public LEngineCodeService LEngineCode { get; }

        public LTightenConfigService LTightenConfig { get; }

        /// <summary>
        /// 本地拧紧结果
        /// 工位级
        /// </summary>
        public LTopEngineResultService LTopEngineResult { get; set; }

        public LocalSQLService()
        {
            try
            { 

                string locadb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AE2Tighten.db");
                FreeHanlder.InitialSQLiteService(locadb);
                EngineService = new EngineInfoService();
                TighteningService = new TightenService();
                TConfigTable = new TConfigService();
                LTopEngineResult = new LTopEngineResultService();
                LEngineType = new LEngineTypeService();
                LEngineCode = new LEngineCodeService();
                LTightenConfig = new LTightenConfigService();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
