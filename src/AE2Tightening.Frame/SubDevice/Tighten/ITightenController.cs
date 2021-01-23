using AE2Tightening.Frame.ViewModel;
using AE2Tightening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Frame
{
    public delegate void OnVinNumberChangedDelegate(string code);
    public interface ITightenController
    {
        event OnVinNumberChangedDelegate OnVinNumberChanged;
        Task<bool> OpenAsync();

        void Close();

        void SendEngineCode(string code);

        bool GetResult();

        void Test(TighteningResultModel model);
    }
}
