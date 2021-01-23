using System;

namespace AE2Devices
{
    public interface IRFIDController:IDevice
    {
        /// <summary>
        /// 标签响应事件
        /// </summary>
        Action<string> OnRfidReaded { get; set; }

        Action<TagData> OnTagDataReaded { get; set; }
        /// <summary>
        /// 获取上次读取的条码
        /// </summary>
        /// <returns></returns>
        string GetReadCode();
    }
}
