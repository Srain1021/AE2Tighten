using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Frame
{
    public class UserInfo
    {
        /// <summary>
        /// 卡片ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否有放行权限
        /// </summary>
        public bool CanRunLine { get; set; }
        /// <summary>
        /// 是否有屏蔽权限
        /// </summary>
        public bool CanSystemShield { get; set; }
        /// <summary>
        /// 刷卡时间
        /// </summary>
        public DateTime SwipeTime { get; set; }

        public UserInfo()
        {
            CanRunLine = false;
            CanSystemShield = false;
            SwipeTime = DateTime.Now;
        }

        public UserInfo Clone()
        {
            return new UserInfo
            {
                CardId = this.CardId,
                Name = this.Name,
                CanRunLine = this.CanRunLine,
                CanSystemShield = this.CanSystemShield,
                SwipeTime = this.SwipeTime
            };
        }
    }
}
