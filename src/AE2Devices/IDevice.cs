using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Devices
{
    public interface IDevice
    {
        bool NetStatus { get; }

        Action<IDevice, bool> NetChangedAction { get; set; }

        bool Open();

        void OpenAsync();

        void Close();

    }
}
