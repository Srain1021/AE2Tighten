using System.Collections.Generic;
using System.Linq;
using AE2Devices;

namespace AE2Tightening.Frame
{
    /// <summary>
    /// 拧紧数据缓存
    /// </summary>
    public class TightenDataCaChe
    {
        public List<TightenData> TightenDatas { get; set; }
        private int tdPoints = 0;
        public TightenDataCaChe()
        {
            TightenDatas = new List<TightenData>();
        }

        public void AddTightenData(TightenData td)
        {
            TightenDatas.Add(td);
        }
        
        public void ReSetTighten(int count)
        {
            tdPoints = count;
            TightenDatas.Clear();
        }

        public bool IsTightenOK()
        {
            if (tdPoints == 0)
                return true;
            if (TightenDatas == null || TightenDatas.Count == 0)
                return false;
            return TightenDatas.Count(t => t.Result == 1) >= tdPoints;
        }
    }
}
