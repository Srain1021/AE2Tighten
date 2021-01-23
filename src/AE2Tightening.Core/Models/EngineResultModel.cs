using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
    [Table("LEngineResult")]
    public class EngineResultModel
    {
        [Key]
        public int TID { get; set; }
        public string EngineCode { get; set; }

        public int TightenResult { get; set; }

        public DateTime? TightenedTime { get; set; }
        /// <summary>
        /// 左稳定杆拧紧结果
        /// </summary>
        public int LStabilizerResult { get; set; }
        /// <summary>
        /// 左稳定杆拧紧结束时间
        /// </summary>
        public DateTime? LStabilizerTighteningTime { get; set; }
        /// <summary>
        /// 右稳定杆结果
        /// </summary>
        public int RStabilizerResult { get; set; }
        /// <summary>
        /// 右稳定杆拧紧结束时间
        /// </summary>
        public DateTime? RStabilizerTighteningTime { get; set; }
        /// <summary>
        /// 左减震器结果
        /// </summary>
        public int LShockResult { get; set; }
        /// <summary>
        /// 左减震器拧紧结束时间
        /// </summary>
        public DateTime? LShockTighteningTime { get; set; }
        /// <summary>
        /// 右减震器结果
        /// </summary>
        public int RShockResult { get; set; }
        /// <summary>
        /// 右减震器拧紧结束时间
        /// </summary>
        public DateTime? RShockTighteningTime { get; set; }

    }
}
