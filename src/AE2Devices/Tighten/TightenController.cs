using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolsAPI.Entities;
using ToolsAPI.Interface;
using ToolsAPI.Value;
using Serilog;
using AE2Tightening.Configura;
using System.Diagnostics;

namespace AE2Devices
{
    internal class TightenController : ITightenController
    {
        static IApi ToolsApi = new ApiClass();
        public bool NetStatus { get; set; }

        public Action<TightenData> OnLastTightenData { get; set; }
        public Action<string> OnVehicleNumberAction { get; set; }
        public Action<bool> NetChangedAction { get; set; }

        private TightenConfig config;
        private IToolsUnit tdTool;

        public TightenController(TightenConfig tdConfig)
        {
            config = tdConfig;
            ToolsApi.GetToolsUnit(config.ToolId.ToString(), config.Host, config.Port, ToolsAPI.Value.ToolsType.PF4000, ref tdTool);
            tdTool.ConnectedChange += TdTool_ConnectedChange;
            tdTool.VINChange += TdTool_VINChange;
            tdTool.ResultChange += TdTool_ResultChange;
        }

        public bool Close()
        {
            try
            {
                if (tdTool != null)
                {
                    tdTool.AutoConnect = false;
                    tdTool.AutoSubVIN = false;
                    tdTool.AutoSubCycData = false;
                    return tdTool.Close() == ToolsRetCode.toolsCloseOK;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        public bool Connect()
        {
            ToolsRetCode ret = ToolsRetCode.toolsNok;
            if (tdTool.Connected == false)
            {
                ret = tdTool.Connect();
                if (ret == ToolsRetCode.toolsOk)
                {
                    SubscribeVehicleNumber();
                    if (SubcribeLastTightenData())
                    {
                        Log.Information("订阅拧紧数据成功");
                    }
                }
            }
            tdTool.AutoConnect = true;
            tdTool.AutoSubVIN = true;
            tdTool.AutoSubCycData = true;
            return ret == ToolsRetCode.toolsOk;
        }
        /// <summary>
        /// 拧紧数据接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TdTool_ResultChange(object sender, EventArgs e)
        {
            try
            {
                object[] objs = (object[])sender;
                string toolsId = objs[0].ToString();
                CycleData cycData = (CycleData)objs[1];
                if (cycData != null)
                {
                    Debug.WriteLine($"接收拧紧数据：{cycData.VIN},{cycData.Torque},{cycData.Angle},{cycData.TighteningStatus},{cycData.TighteningID},{cycData.TimeStamp}");
                    TightenData data = new TightenData
                    {
                        EngineCode = cycData.VIN,
                        Pset = cycData.PSetID,
                        BoltNo = cycData.BatchCount,
                        BoltCount = cycData.BatchSize,
                        Torque = cycData.Torque,
                        Angle = cycData.Angle,
                        Result = cycData.TighteningStatus,
                        JobResult = cycData.BatchStatus,
                        TighteningId = cycData.TighteningID,
                        TightenTime = DateTime.Now
                    };
                    OnLastTightenData?.Invoke(data);
                }
                else
                {
                    Log.Warning("接收到拧紧数据为空");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "拧紧数据接收报错");
            }
        }
        /// <summary>
        /// 拧紧机条码更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TdTool_VINChange(object sender, EventArgs e)
        {
            try
            {
                object[] objArr = (object[])sender;
                string toolsID = objArr[0].ToString();
                string vin = objArr[1].ToString().Replace("\r", "")?.Replace("\n", "")?.Replace("\0", "")?.Replace("\t", "").TrimEnd();
                OnVehicleNumberAction?.Invoke(vin);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "拧紧条码接收报错");
            }
        }

        /// <summary>
        /// 拧紧机网络状态更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TdTool_ConnectedChange(object sender, EventArgs e)
        {
            try
            {
                object[] objArr = (object[])sender;
                string toolsID = objArr[0].ToString();
                bool connected = (bool)objArr[1];
                NetChangedAction?.Invoke(connected);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "拧紧通讯状态接收报错");
            }
        }

        public bool DisableTightenTool()
        {
            if (tdTool != null && tdTool.Connected)
            {
                return tdTool.DisableTool() == ToolsRetCode.toolsOk;
            }
            return false;
        }

        public bool EnableTightenTool()
        {
            if (tdTool != null && tdTool.Connected)
            {
                return tdTool.EnableTool() == ToolsRetCode.toolsOk;
            }
            return false;
        }

        public bool SelectJob(int jobNum)
        {
            if (tdTool != null && tdTool.Connected)
            {
                return tdTool.SelectJob(jobNum) == ToolsRetCode.toolsOk;
            }
            return false;
        }

        public bool SetPSet(int pSetNum)
        {
            if (tdTool != null && tdTool.Connected)
            {
                return tdTool.SelectPSet(pSetNum) == ToolsRetCode.toolsOk;
            }
            return false;
        }

        public bool SetVehicleNumber(string vin)
        {
            if (tdTool != null && tdTool.Connected)
            {
                return tdTool.SetVIN(vin) == ToolsRetCode.toolsOk;
            }
            return false;
        }

        public bool SubcribeLastTightenData()
        {
            if (tdTool != null && tdTool.Connected)
            {
                return tdTool.SubscribeLastResult(MIDRevision.V002) == ToolsRetCode.toolsOk;
            }
            return false;
        }

        public bool SubscribeVehicleNumber()
        {
            if (tdTool != null && tdTool.Connected)
            {
                return tdTool.SubscribeVIN() == ToolsRetCode.toolsOk;
            }
            return false;
        }
    }
}
