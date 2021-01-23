using AE2Tightening.Frame.ViewModel;
using AE2Tightening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Frame.Data
{
    public class RepositoryCache
    {
        /// <summary>
        /// 发动机机型数据
        /// </summary>
        public List<EngineTypeModel> EngineTypes { get; set; }
        /// <summary>
        /// 变速箱类型
        /// </summary>
        public List<EngineAutoTypeModel> AutoTypes { get; set; }
        /// <summary>
        /// 零件号绑定对应关系
        /// </summary>
        public List<EnginePnoModel> EnginePnos { get; set; }
        //派生和车型的对应关系？
        //public List<LEngineTypeModel> LEngineTypes { get; set; }
      
        /// <summary>
        /// 获取发动机机型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public EngineTypeModel GetEngineType(string code)
        {
            return EngineTypes?.FirstOrDefault(x =>
                    code.GetString(x.FeatureIndex.Split(',').Select(xx => int.Parse(xx) - 1).ToArray())
                        .Equals(x.FeatureCode, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// 获取发动机车型？
        /// </summary>
        /// <param name="mto"></param>
        /// <returns></returns>
        public EngineAutoTypeModel GetTCaseType(string mto)
        {
            return AutoTypes?.FirstOrDefault(x =>
                    mto.GetString(x.DeriveFeatureIndex.Split(',').Select(xx => int.Parse(xx) - 1).ToArray())
                        .Equals(x.DeriveFeatureCode, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
