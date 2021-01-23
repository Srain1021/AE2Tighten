using AE2Tightening.Models;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;

namespace AE2Tightening.Services
{
    public class DerivePnoService: ServiceBase
    {
        public List<DerivePnoModel> GetAll(string _ID)
        {
            return this.Invoke((c) =>
            {
                return c.Query<DerivePnoModel>(
                    "select * from Match_DerivePNO where StationID=@StationID and State=1", 
                    new { StationID = _ID }).ToList();
            });
        }


        public DerivePnoModel Get(string code, string derive)
        {
            if (code == null) 
                throw new System.ArgumentNullException(nameof(code));
            if (derive == null) 
                throw new System.ArgumentNullException(nameof(derive));

            return this.Invoke((c) =>
            {
                return c.QueryFirstOrDefault<DerivePnoModel>(
                    "select * from Match_DerivePNO where PNO=@code and DeriveFeatureCode=@derive and State=1", 
                    new { code = code, derive = derive });
            });
        }
    }
}
