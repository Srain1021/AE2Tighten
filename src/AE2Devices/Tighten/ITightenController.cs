using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Devices
{
    public interface ITightenController
    {
        bool NetStatus { get; }

        #region "事件"区

        Action<TightenData> OnLastTightenData { get; set; }

        Action<string> OnVehicleNumberAction { get; set; }

        Action<bool> NetChangedAction { get; set; }
        
        #endregion

        bool Connect();
        bool Close();

        bool SubcribeLastTightenData();

        bool SubscribeVehicleNumber();

        /// <summary>
        /// 设置发动机码(条码)
        /// </summary>
        /// <param name="vin">条码数据</param>
        /// <returns></returns>
        bool SetVehicleNumber(string vin);

        /// <summary>
        /// 设置PSet(ParameterSet)值.即,内置拧紧程序序号
        /// </summary>
        /// <param name="pSetNum"></param>
        /// <returns></returns>
        bool SetPSet(int pSetNum);

        bool SelectJob(int jobNum);

        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        bool EnableTightenTool();

        /// <summary>
        /// 锁枪
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        bool DisableTightenTool();

    }
}
