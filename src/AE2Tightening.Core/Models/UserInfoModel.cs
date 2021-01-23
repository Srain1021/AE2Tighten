using Dapper.Contrib.Extensions;
using System;

namespace AE2Tightening.Models
{
    [Table("UserInfo")]
    public class UserInfoModel
    {
        [Key]
        public int TID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string RoleName { get; set; }
        public string Bak { get; set; }
        public string AuthorizationID { get; set; }
    }
}
