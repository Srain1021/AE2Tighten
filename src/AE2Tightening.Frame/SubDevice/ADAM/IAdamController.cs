using System;

namespace AE2Tightening.Frame
{
    public delegate void SensorTriggerDelegate(EnumSensor sensor);
    public interface IAdamController
    {
        event SensorTriggerDelegate OnSensorTrigger;

        Action<bool> OnNetChanged { get; set; }

        void AlarmWarning(bool state);

        void Start();

        void Close();

        void Reset();

        void TestToStation(bool state);

        void TestOutStation(bool state);

    }
}
