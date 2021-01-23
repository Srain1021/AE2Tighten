using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Devices
{
    public interface IOpcController : IDevice
    {
        /// <summary>
        /// 线体状态变动
        /// </summary>
        Action<bool> PassChangedAction { get; set; }
        /// <summary>
        /// PLC端屏蔽信号变动
        /// </summary>
        Action<bool> ShieldChangedAction { get; set; }
        /// <summary>
        /// 开停线控制
        /// </summary>
        /// <param name="isStop">是否停线</param>
        /// <returns></returns>
        bool Pass();

        bool GetShieldValue();

    }
}
