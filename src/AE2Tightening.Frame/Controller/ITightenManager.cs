using System;
using System.Threading.Tasks;
using AE2Devices;
using AE2Tightening.Lite;

namespace AE2Tightening.Frame
{
    public delegate void OnVinNumberChangedDelegate(int toolId,string code);

    public interface ITightenManager
    {
        event OnVinNumberChangedDelegate VinNumberChangedEvent;
        /// <summary>
        /// 网络状态变更
        /// </summary>
        Action<int,bool> NetChangedAction { get; set; }
        /// <summary>
        /// 输出拧紧数据
        /// </summary>
        Action<int, TightenData> OnLastTightenAction { get; set; }

        /// <summary>
        /// 当前拧紧机需要拧紧的螺丝数量
        /// </summary>
        Action<int, int> OnNeedTightenBoltCountAction { get; set; }

        /// <summary>
        /// 拧紧工作结束事件
        /// </summary>
        Action<int, bool, TightenData> OnWorkUnitFinishedAction { get; set; }

        /// <summary>
        /// 设置拧紧点信息(不发条码)
        /// </summary>
        /// <param name="code"></param>
        void InitTightenInfo(string engineCode, LTightenConfigModel tConfig);
        /// <summary>
        ///  向拧紧机发送条码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool SetTighenCode(string code);

        /// <summary>
        /// 当前拧紧机需要拧紧的螺栓数
        /// </summary>
        /// <param name="EngineCode"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        int GetBoltCount();

        /// <summary>
        /// 获取拧紧结果
        /// </summary>
        /// <returns></returns>
        bool GetResult();

        bool Connect();

        int GetToolsID();

        bool EnableTool(bool status);
    }
}
