using System;

namespace AE2Devices
{
    /// <summary>
    /// 拧紧机输出的结果
    /// </summary>
    public class TightenData
    {
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineCode { get; set; }
        /// <summary>
        /// 程序号
        /// </summary>
        public int Pset { get; set; }
        /// <summary>
        /// 螺栓序号
        /// </summary>
        public int BoltNo { get; set; }
        /// <summary>
        /// 螺栓总数
        /// </summary>
        public int BoltCount { get; set; }
        /// <summary>
        /// 力矩
        /// </summary>
        public double Torque { get; set; }
        /// <summary>
        /// 角度
        /// </summary>
        public decimal Angle { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public int Result { get; set; }
        /// <summary>
        /// 批次总结果
        /// </summary>
        public int JobResult { get; set; }
        /// <summary>
        /// 拧紧时间
        /// </summary>
        public DateTime TightenTime { get; set; }

        /// <summary>
        /// 拧紧ID
        /// </summary>
        public int TighteningId { get; set; }
        public TightenData()
        {
            TightenTime = DateTime.Now;
            JobResult = 0;
        }
    }
}
