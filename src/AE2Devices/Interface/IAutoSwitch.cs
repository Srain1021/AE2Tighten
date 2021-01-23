using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Devices
{
    public interface IAutoSwitch
    {
        void SetAutoMode(bool isAuto);

        bool GetAutoModel();
    }
}
