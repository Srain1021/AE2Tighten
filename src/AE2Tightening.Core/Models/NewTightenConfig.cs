using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper.Contrib.Extensions;

namespace AE2Tightening.Models
{
    [Table("NewTightenConfig")]
    public class NewTightenConfig
    {
        [Key]
        public int TID { get; set; }
        public string EngineFeatureCode { get; set; }
        public string EngineFeatureIndex { get; set; }
        public string StationId { get; set; }
        public int TightenPointNum { get; set; }

        //准备添加的字段
        /// <summary>
        /// 拧紧枪序号
        /// 同一个sationId下可以有N把枪
        /// </summary>
        public int? ToolId { get; set; } = 0; //默认给0,但是0是不合法的,要从1开始

        /// <summary>
        /// Job序号
        /// </summary>
        public int? JobNum { get; set; }

        /// <summary>
        /// 螺丝1对应的Pset
        /// </summary>
        public int? BoltPset1 { get; set; }
        
        /// <summary>
        /// 螺丝2对应的Pset
        /// </summary>
        public int? BoltPset2 { get; set; }

        /// <summary>
        /// 螺丝3对应的Pset
        /// </summary>
        public int? BoltPset3 { get; set; }

        /// <summary>
        /// 螺丝4对应的Pset
        /// </summary>
        public int? BoltPset4 { get; set; }

        /// <summary>
        /// 螺丝5对应的Pset
        /// </summary>
        public int? BoltPset5 { get; set; }

        /// <summary>
        /// 螺丝6对应的Pset
        /// </summary>
        public int? BoltPset6 { get; set; }

        /// <summary>
        /// 螺丝6对应的Pset
        /// </summary>
        public int? BoltPset7 { get; set; }

        /// <summary>
        /// 螺丝6对应的Pset
        /// </summary>
        public int? BoltPset8 { get; set; }

        /// <summary>
        /// 螺丝6对应的Pset
        /// </summary>
        public int? BoltPset9 { get; set; }

        /// <summary>
        /// 螺丝6对应的Pset
        /// </summary>
        public int? BoltPset10 { get; set; }

    }
}
