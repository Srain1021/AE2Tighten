using AE2Tightening.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AE2Tightening.Services
{
    public class EngineQueueService: ServiceBase
    {
      
        public EngineQueueModel Get(string code)
        {
            if (code == null) throw new System.ArgumentNullException(nameof(code));

            return this.Invoke((c) =>
            {
                return c?.QueryFirstOrDefault<EngineQueueModel>("select top 1 * from LEngineQueue where EngineCode=@EngineCode", new { EngineCode = code });
            });
        }

        public EngineQueueModel GetMaxNo()
        {

            return this.Invoke((c) =>
            {
                return c.QueryFirstOrDefault<EngineQueueModel>("select top 1 *  from LEngineQueue order by tNo desc");
            });
        }

        public List<EngineQueueModel> GetS(string code)
        {
            if (code == null) throw new System.ArgumentNullException(nameof(code));

            return this.Invoke((c) =>
            {
                return c.Query<EngineQueueModel>($"select * from LEngineQueue where TNO>=(SELECT TNO FROM LEngineQueue where EngineCode=@EngineCode) order by TNO desc", new { EngineCode = code }).ToList();
            });
        }


        public bool Insert(EngineQueueModel model)
        {
            if (model == null) throw new System.ArgumentNullException(nameof(model));

            return this.Invoke((c) =>
            {
                return c?.Insert(model) > 0;
            });
        }
        public bool Update(EngineQueueModel model)
        {
            if (model == null) throw new System.ArgumentNullException(nameof(model));

            return this.Invoke((c) =>
            {
                return c.Update(model);
            });
        }


        public List<EngineQueueModel> getSelectQueue(string stationID1, string stationID2, string orderby = "TNO DESC")
        {

            if (stationID1 == null) throw new System.ArgumentNullException(nameof(stationID1));
            if (stationID2 == null) throw new System.ArgumentNullException(nameof(stationID2));
            return this.Invoke((c) =>
            {
                //select * from LEngineQueue where TNO <=(SELECT TNO FROM LEngineQueue WHERE StationID='ST37OilFilling') and TNO >=(SELECT TNO FROM LEngineQueue WHERE StationID='ST38Inspection') order by tno desc;
                return c.Query<EngineQueueModel>($"select * from LEngineQueue where TNO <=(SELECT TNO FROM LEngineQueue WHERE StationID=@StationID1) and TNO >=(SELECT TNO FROM LEngineQueue WHERE StationID=@StationID2) order by {orderby};" , new { StationID1 = stationID1, StationID2 = stationID2 })?.ToList();
            });
        }

        public List<string> UpcomingQueueList(string _CurSid,string _BeferSid)
        {
            return this.Invoke((c) =>
            {
                //return c.Query<string>("select EngineCode from LEngineQueue where TNO >(SELECT TNO FROM LEngineQueue WHERE StationID='ST2TopLineWrite') and  TNO <(SELECT TNO FROM LEngineQueue WHERE StationID='ST1RGV')")?.ToList();

                return c.Query<string>($"select EngineCode from LEngineQueue where TNO >(SELECT q.TNO FROM LEngineQueue q join StationInfo si on q.EngineCode=si.LastCode and si.StationID='{_CurSid}') and  TNO <(SELECT q.TNO FROM LEngineQueue q join StationInfo si on q.EngineCode=si.LastCode and si.StationID='{_BeferSid}')")?.ToList();
            });
        }

        public List<string> QueueList(string _query)
        {
            return this.Invoke((c) =>
            {
                return c.Query<string>(_query)?.ToList();
            });
        }

        public List<string> PassedQueueList(string _CurSid, string _NextSid)
        {
            return this.Invoke((c) =>
            {
                return c.Query<string>($"select EngineCode from LEngineQueue where TNO <(SELECT q.TNO FROM LEngineQueue q join StationInfo si on q.EngineCode=si.LastCode and si.StationID='{_CurSid}') and  TNO >(SELECT q.TNO FROM LEngineQueue q join StationInfo si on q.EngineCode=si.LastCode and si.StationID='{_NextSid}') order by TNO desc")?.ToList();
            });
        }


        public List<EngineQueueModel> List(string _query)
        {
            return this.Invoke((c) =>
            {
                return c.Query<EngineQueueModel>(_query)?.ToList();
            });
        }

        public List<string> GetIncomingCode(string stationID1, string stationID2, string orderby = "TNO DESC")
        {
            if (stationID1 == null) throw new System.ArgumentNullException(nameof(stationID1));
            if (stationID2 == null) throw new System.ArgumentNullException(nameof(stationID2));
            return this.Invoke((c) =>
            {
                return c.Query<string>($"select EngineCode from LEngineQueue where TNO <(SELECT TNO FROM LEngineQueue WHERE StationID=@StationID1) and TNO >(SELECT TNO FROM LEngineQueue WHERE StationID=@StationID2) order by {orderby};", new { StationID1 = stationID1, StationID2 = stationID2 })?.ToList();
            });
        }

        public bool UpdateQueue(string code)
        {
            string sql = $"update LEngineQueue set TNo=TNo+1 where TNo>=(SELECT TNo FROM LEngineQueue where EngineCode='{code}');";

           
            return this.Invoke((c) =>
            {
               // return c.Update<string>($"update LEngineQueue set TNo=TNo+1 where TNO>=(SELECT TNO FROM LEngineQueue where EngineCode='{code}';");
                return c.Execute(sql) > 0;
            });
        }

        public string getMax(string code)
        {
            //string sql = $"select max(RIGHT([EngineCode],4)) from [LEngineQueue] where LEFT([EngineCode],8)='{code}'";
            //return this.Invoke((c) =>
            //{
            //    return c.Query<string>($"select max(RIGHT([EngineCode],4)) from [LEngineQueue] where LEFT([EngineCode],8)=@code;",new { code = code})
            //});

            if (code == null) throw new System.ArgumentNullException(nameof(code));

            return this.Invoke((c) =>
            {
                return c.QueryFirstOrDefault<string>($"select max(RIGHT([EngineCode],4)) from [LEngineQueue] where LEFT([EngineCode],8)=@code;", new { code = code });
            });
        }

        public bool InsertQueue(EngineQueueModel model)
        {
             if (model == null) throw new System.ArgumentNullException(nameof(model));
            string sql =$"insert into LEngineQueue(TNo,EngineCode,RepairStatus) values(@TNo,@EngineCode,@RepairStatus);";

            return this.Invoke((c) =>
            {
                return c.Execute(sql,new { Tno=model.TNo,EngineCode = model.EngineCode, RepairStatus =0}) > 0;
            });
        }
    }
}

