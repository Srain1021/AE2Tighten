using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Frame
{
    public interface IOpcController
    {
        void Open();

        void Close();

        Task<bool> StopLine();

        Task<bool> RunLine();

        void ShieldPLC(bool state);
    }
}
