using AE2Tightening.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace AE2Tightening.Services
{
    public class UserInfoService : ServiceBase
    {
        public UserInfoModel Get(string authorID)
        {
            if (authorID == null)
                throw new System.ArgumentNullException(nameof(authorID));

            return this.Invoke((c) =>
            {
                return c.QueryFirstOrDefault<UserInfoModel>(
                    "select top 1 * from UserInfo where AuthorizationID=@AuthorizationID",
                    new { AuthorizationID = authorID });
            });
        }
    }
}
