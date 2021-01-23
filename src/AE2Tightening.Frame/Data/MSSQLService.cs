using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AE2Tightening.Configura;
using AE2Tightening.Frame.ViewModel;
using AE2Tightening.Models;
using AE2Tightening.Services;
using Serilog;

namespace AE2Tightening.Frame.Data
{
    /// <summary>
    /// 对Dapper类的一个总装
    /// 里面的各个xxxService对应数据库里的表.
    /// 类似与EF的DBContext类的DataSet<T>
    /// </summary>
    public class MSSQLService
    {
        public EngineTypeService EngineType { get;  }

        public EngineQueueService EngineQueue { get;  }

        public EngineAutoTypeService EngineAutoType { get;  }

        public TighteningResultService TightenService { get;  }

        public EngineResultService EngineResult { get;}

        public DerivePnoService DerivePno { get;  }

        public StationService StationService { get; }

        public MaterialMetasService MaterialMetas { get; }

        public RepositoryCache DbDataCache { get; }

        public TightenConfigService TightenConfig { get; }
        
        public EngineInfoService EngineInfo { get; }

        public TopEngineResultService TopEngineResult { get; }

        public TopLineEngineQueueService TopLineEngineQueue { get; }

        public SysMonitorAlarmService SysMonitorAlarm { get; }

        public RFIDPointInfoService RFIDPointInfo { get;}

        public CardAuthorService CardAuthor { get; set; }
        public MSSQLService()
        {
            EngineType = new EngineTypeService();
            EngineQueue = new EngineQueueService();
            EngineAutoType = new EngineAutoTypeService();
            TightenService = new TighteningResultService();
            EngineResult = new EngineResultService();
            DbDataCache = new RepositoryCache();
            DerivePno = new DerivePnoService();
            StationService = new StationService();
            MaterialMetas = new MaterialMetasService();
            TightenConfig = new TightenConfigService();
            EngineInfo = new EngineInfoService();
            TopEngineResult = new TopEngineResultService();
            TopLineEngineQueue = new TopLineEngineQueueService();
            SysMonitorAlarm = new SysMonitorAlarmService();
            RFIDPointInfo = new RFIDPointInfoService();
            CardAuthor = new CardAuthorService();
        }
        /// <summary>
        /// 数据缓存
        /// </summary>
        public async void CreateDataCache()
        {
            try
            {
                DbDataCache.EngineTypes = await GetEngineTypes();
                DbDataCache.AutoTypes = await GetAllAutoTypes();
            } 
            catch (Exception ex)
            {
                Log.Error("程序启动时，缓存机型数据异常", ex);
            }
           
        }

        /// <summary>
        /// 获取发动机机型缓存
        /// </summary>
        /// <returns></returns>
        public async Task<List<Models.EngineTypeModel>> GetEngineTypes()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return EngineType.GetAll();
                }
                catch (Exception e)
                {
                    Log.Error("缓存发动机机型数据时异常，", e);
                    return null;
                }
            });
        }

        /// <summary>
        /// 获取发动机类型的数据缓存
        /// </summary>
        /// <returns></returns>
        public async Task<List<EngineAutoTypeModel>> GetAllAutoTypes()
        {
            return await Task.Run(() =>
            {
                try
                {
                    return EngineAutoType.GetAll();
                }
                catch (Exception ex)
                {
                    Log.Error("缓存变速箱类型数据时异常", ex);
                    return null;
                }
            });
        }
        /// <summary>
        /// 获取发动机参数
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public EngineViewModel GetEngineOther(string code, string mto)
        {
            TopLineEngineQueueModel queueModel = new TopLineEngineQueueModel
            {
                EngineCode = code,
                EngineMTO = mto
            };
            if (!string.IsNullOrEmpty(code))
            {
                EngineTypeModel engineType = DbDataCache.GetEngineType(code);
                queueModel.EngineType = engineType?.TypeName;
                if (!string.IsNullOrEmpty(mto))
                {
                    EngineAutoTypeModel autoType = DbDataCache.GetTCaseType(mto);
                    queueModel.TCaseType = autoType?.TCaseType;
                }
                else
                {
                    queueModel = TopLineEngineQueue.Get(code);
                }
            }
            string lmpstr = queueModel.EngineType + " " + queueModel.TCaseType;

            return EngineViewModel.CreateModel(queueModel.EngineCode, queueModel.EngineMTO, queueModel.EngineType, lmpstr);
        }

        /// <summary>
        /// 保存减震器码绑定信息
        /// </summary>
        /// <param name="model"></param>
        public void SaveMaterialMetas(MaterialMetasModel model)
        {
            try
            {
                if (model.Id == 0)
                {
                    model.CreateTime = DateTime.Now;
                    model.ModifiedTime = DateTime.Now;
                    model.Id = MaterialMetas.Insert(model);
                }
                else
                {
                    model.ModifiedTime = DateTime.Now;
                    MaterialMetas.Update(model);
                }
            }
            catch (Exception ex)
            {
                Log.Error("存储条码绑定信息失败",ex);
            }
        }
    }
}
