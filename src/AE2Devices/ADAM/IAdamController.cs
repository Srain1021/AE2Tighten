using System;

namespace AE2Devices
{
    public delegate void SensorTriggerDelegate(EnumSensor sensor);
    public interface IAdamController : IDevice
    {
        event SensorTriggerDelegate OnSensorTrigger;
        /// <summary>
        /// 声光报警（带声音）
        /// </summary>
        /// <param name="state"></param>
        bool AlarmWarning(bool state);
        /// <summary>
        /// 蜂鸣报警（声音较大）
        /// </summary>
        /// <param name="state"></param>
        bool Beer(bool state);

        bool Warn(bool state);
    }
}
