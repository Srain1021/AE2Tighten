using AE2Tightening.Frame.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Frame
{
   // public delegate void OnRFIDReadedDelegate(string code);
    public interface IRFIDController
    {
        Action<bool> OnNetChanged { get; set; }
        /// <summary>
        /// 标签响应事件
        /// </summary>
        Action<string> OnRfidReaded { get; set; }
        /// <summary>
        /// 获取上次读取的条码
        /// </summary>
        /// <returns></returns>
        string GetReadCode();
        /// <summary>
        /// 启动设备链接
        /// </summary>
        void Start();
        /// <summary>
        /// 关闭设备通讯
        /// </summary>
        void CloseAsync();
        /// <summary>
        /// 测试
        /// </summary>
        void Test();
    }
}
