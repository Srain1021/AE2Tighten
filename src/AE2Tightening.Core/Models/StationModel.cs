using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
    /// <summary>
    /// StationInfo
    /// </summary>
    [Table("StationInfo")]
    public class StationModel
    {
        [Key]
        public int TID { get; set; }

        /// <summary>
        /// 工位编号
        /// </summary>
        public string StationID { get; set; }

        /// <summary>
        /// 上一工件条码号
        /// </summary>
        public string LastCode { get; set; }

        /// <summary>
        /// 上一工件完成标识，0表示NG，1表示Ok
        /// </summary>
        public int? LastResult { get; set; }

        /// <summary>
        /// 上一工件的完成时间
        /// </summary>
        public DateTime? LastTime { get; set; }
 
    }
}
