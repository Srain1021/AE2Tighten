using AE2Tightening.Frame.ViewModel;
using AE2Tightening.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Frame
{
    public class TightenDataCaChe
    {
        public List<TighteningResultModel> TightenDatas { get; set; }
        private int tdPoints = 0;
        public TightenDataCaChe(int points)
        {
            tdPoints = points;
            TightenDatas = new List<TighteningResultModel>();
        }

        public void AddTightenData(TighteningResultModel td)
        {
            TightenDatas.Add(td);
        }

        public void ClearTightenData()
        {
            TightenDatas.Clear();
        }

        public bool IsTightenOK()
        {
            if (TightenDatas == null || TightenDatas.Count == 0)
                return false;
            return TightenDatas.Count(t => t.Result == 1) >= tdPoints ;
        }
    }
}
