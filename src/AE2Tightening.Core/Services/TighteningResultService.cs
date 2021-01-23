using AE2Tightening.Models;
using Dapper.Contrib.Extensions;

namespace AE2Tightening.Services
{
    public class TighteningResultService :ServiceBase
    {
        public bool Insert(TighteningResultModel model)
        {
            if (model == null)
                return false;
            return Invoke((c) => c?.Insert(model) > 0);
        }

    }
}
