using System;
using AE2Tightening.Configura;

namespace AE2Devices
{
    public interface IScanController
    {
        SerialConfig Config { get; }

        bool IsOpen { get; }

        bool Open();

        void Close();

        Action<string> OnScanCoded { get; set; }
    }
}
