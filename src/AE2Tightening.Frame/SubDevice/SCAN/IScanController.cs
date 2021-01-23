
using AE2Tightening.Configura;
using System;
using System.CodeDom;

namespace AE2Tightening.Frame
{
    public delegate void OnScanCodeDelegate(string code,EnumCodeType codeType);
    public interface IScanController
    {
        SerialConfig Config { get; }

        bool IsOpen { get; }

        bool Open();

        void Close();

        event OnScanCodeDelegate OnScanCoded;
    }
}
