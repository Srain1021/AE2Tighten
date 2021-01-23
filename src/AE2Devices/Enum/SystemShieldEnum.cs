using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Devices
{
    public enum StationEnum
    {
        /// <summary>
        /// 上装写入站
        /// </summary>
        ST2TopLineWrite,
        /// <summary>
        /// 线束
        /// </summary>
        ST3WireHarness,
        /// <summary>
        /// 副车架结合
        /// </summary>
        ST4SubFrame,
        /// <summary>
        /// 减震器
        /// </summary>
        ST5Shock,
        /// <summary>
        /// 稳定杆
        /// </summary>
        ST6Stabilizer,
        /// <summary>
        /// 变速箱加注
        /// </summary>
        ST7FillLine,
        /// <summary>
        /// 质量门
        /// </summary>
        ST6AEOFF,
        /// <summary>
        /// 压缩机安装
        /// </summary>
        ST8Compressor,
    }
}
