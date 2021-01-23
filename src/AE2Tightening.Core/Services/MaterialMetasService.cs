using AE2Tightening.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE2Tightening.Services
{
    public class MaterialMetasService : ServiceBase
    {
        public int Insert(MaterialMetasModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            return this.Invoke(c =>
            {
                return (int)c.Insert(model);
            });
        }

        public MaterialMetasModel Get(string engineCode)
        {
            return Invoke(c =>
            {
                return c.QueryFirstOrDefault<MaterialMetasModel>("select * from MaterialMetas where EngineCode = @engineCode", new { engineCode });
            });
        }

        public bool Update(MaterialMetasModel model)
        {
            if(model == null)
                throw new ArgumentNullException(nameof(model));
            return Invoke(c =>
            {
                return c.Update(model);
            });
        }
    }
}
