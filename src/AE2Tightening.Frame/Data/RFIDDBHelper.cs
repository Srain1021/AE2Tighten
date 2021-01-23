using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE2Tightening.Lite;
using AE2Tightening.Models;
using Serilog;

namespace AE2Tightening.Frame.Data
{
    public class RFIDDBHelper
    {
        public static readonly MSSQLService MSSQLHandler = new MSSQLService();

        public static readonly LocalSQLService LocalSQLHandler = new LocalSQLService();


        /// <summary>
        /// 获取发动机参数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static EngineViewModel GetEngineOther(string code, string mto)
        {
            TopLineEngineQueueModel queueModel = new TopLineEngineQueueModel
            {
                EngineCode = code,
                EngineMTO = mto
            };
            if (!string.IsNullOrEmpty(code))
            {
                LEngineTypeModel engineType = LocalSQLHandler.LEngineType.Get(code);
                queueModel.EngineType = engineType?.TypeName;
                if (!string.IsNullOrEmpty(mto))
                {
                    LEngineCodeModel autoType = LocalSQLHandler.LEngineCode.Get(mto);
                    queueModel.TCaseType = autoType?.TCaseType;
                }
                else
                {
                    queueModel = MSSQLHandler.TopLineEngineQueue.Get(code);
                }
            }
            string lmpstr = queueModel.EngineType + " " + queueModel.TCaseType;

            return EngineViewModel.CreateModel(queueModel.EngineCode, queueModel.EngineMTO, queueModel.EngineType, lmpstr);
        }

        /// <summary>
        /// 机型参数同步
        /// </summary>
        /// <returns></returns>
        public static bool DownLoadServerConfig(string stationId)
        {
            try
            {
                #region 机型
                List<EngineTypeModel> lstEngType = MSSQLHandler.EngineType.GetAll();
                List<LEngineTypeModel> lstLEngType = lstEngType.Select(x => new LEngineTypeModel
                {
                    FeatureCode = x.FeatureCode,
                    FeatureIndex = x.FeatureIndex,
                    TypeName = x.TypeName
                }).ToList();
                LocalSQLHandler.LEngineType.DeleteAll();
                bool ret = LocalSQLHandler.LEngineType.InsertList(lstLEngType);
                #endregion
                #region 机种
                List<EngineAutoTypeModel> lstEngCode = MSSQLHandler.EngineAutoType.GetAll();
                List<LEngineCodeModel> lstLEngCode = lstEngCode.Select(x => new LEngineCodeModel
                {
                    FeatureCode = x.FeatureCode,
                    FeatureIndex = x.FeatureIndex,
                    DeriveFeatureCode = x.DeriveFeatureCode,
                    DeriveFeatureIndex = x.DeriveFeatureIndex,
                    TCaseType = x.TCaseType
                }).ToList();
                LocalSQLHandler.LEngineCode.DeleteAll();
                bool ret1 = LocalSQLHandler.LEngineCode.InsertList(lstLEngCode);
                #endregion
                #region 拧紧
                List<NewTightenConfig> lstTd = MSSQLHandler.TightenConfig.GetNew(stationId);
                List<LTightenConfigModel> lstLtd = lstTd.Select(t => new LTightenConfigModel
                {
                    StationId = t.StationId,
                    FeatureCode = t.EngineFeatureCode,
                    FeatureIndex= t.EngineFeatureIndex,
                    ToolId = t.ToolId??0,
                    TightenPointNum = t.TightenPointNum,
                    JobId = t.JobNum,
                    BoltPset1 = t.BoltPset1,
                    BoltPset2 = t.BoltPset2,
                    BoltPset3 = t.BoltPset3,
                    BoltPset4 = t.BoltPset4,
                    BoltPset5 = t.BoltPset5,
                    BoltPset6 = t.BoltPset6,
                    BoltPset7 = t.BoltPset7,
                    BoltPset8 = t.BoltPset8,
                    BoltPset9 = t.BoltPset9,
                    BoltPset10 = t.BoltPset10
                }).ToList();
                LocalSQLHandler.LTightenConfig.DeleteAll();
                bool ret2 = LocalSQLHandler.LTightenConfig.InsertList(lstLtd);
                #endregion
                return ret && ret1 && ret2;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "同步参数报错");
                return false;
            }
        }


    }
}
