using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AE2Tightening.Models;
using Dapper;

namespace AE2Tightening.Services
{
    public class CardAuthorService : ServiceBase
    {

        public CardAuthorModel GetByNo(string no)
        {
            return this.Invoke((c) =>
            {
                return c?.QueryFirstOrDefault<CardAuthorModel>("select top 1 * from CardAuthor with(nolock) where CardNo = @no", new { no });
            });
        }
    }
}
