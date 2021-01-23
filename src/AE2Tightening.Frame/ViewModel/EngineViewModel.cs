using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE2Tightening.Configura;

namespace AE2Tightening.Frame
{
    public class EngineViewModel
    {
        public bool IsOut { get; set; }
        private string engineCode;
        private string engineMto;
        private string engineName;
        private string engineType;
        /// <summary>
        /// 发动机码
        /// </summary>
        public string EngineCode { get => engineCode; }
        /// <summary>
        /// 派生
        /// </summary>
        public string EngineMTO { get=>engineMto; }
        /// <summary>
        /// 机型
        /// </summary>
        public string EngineType { get=>engineType;  }
        /// <summary>
        /// 机型 + 类型
        /// </summary>
        public string EngineName { get=>engineName;  }//EngineType + TCaseType
       

        public static EngineViewModel CreateModel(string code, string mto = "", string type = "", string name = "")
        {
            return new EngineViewModel
            {
                engineCode = code,
                engineMto = mto,
                engineName = name,
                engineType = type
            };
        }

        public EngineViewModel Clone()
        {
            var model = new EngineViewModel();
            model.engineCode = engineCode;
            model.engineMto = engineMto;
            model.engineName = engineName;
            model.engineType = engineType;
            return model;
        }
    }
}
