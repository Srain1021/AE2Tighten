using AE2Devices;
using AE2Tightening.Configura;
using AE2Tightening.Frame.Data;
using AE2Tightening.Lite;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AE2Tightening.Frame
{
    public class MyTightenManager : ITightenManager
    {
        public ITightenController TightenToolClient { get; set; }//拧紧机通讯接口
        // private readonly Logging Log;//日志接口
        public TightenDataCaChe TightenDatas { get; }//拧紧数据缓存
        /// <summary>
        /// 配置信息
        /// </summary>
        private TightenConfig tdConfig;

        private LTightenConfigModel newTightenConfig = null;

        public string CurrentStationId { get; private set; } = string.Empty;
        public string CurrentEngineCode { get; set; } = string.Empty;

        private bool netStatus = false;
        private int reconnNum = 0;
        private bool isAutoModel = false;

        public static object lockObj = new object();
        private static object netStatusLockObj = new object();


        /// <summary>
        /// 拧紧机网络状态
        /// Thread Safe
        /// </summary>
        public bool NetStatus
        {
            get
            {
                lock (netStatusLockObj)
                {
                    return netStatus;
                }
            }
            private set
            {
                lock (netStatusLockObj)
                {
                    netStatus = value;
                }
            }
        }

        /// <summary>
        /// 当前发动机条码
        /// </summary>
        public string CurrentCode { get; set; }

        public int CurrentPointNum { get; set; }

        public bool JobWorkModel { get; set; }

        /// <summary>
        /// 工作是否结束，OK or NG状态都可能结束
        /// </summary>
        public bool WorkingFinished { get; set; }

        /// <summary>
        /// 单颗螺丝拧紧次数
        /// 新条码进入时归0,拧紧一次+1,判断拧紧结果OK时归0
        /// </summary>
        public int TightenCount { get; set; } = 0;

        /// <summary>
        /// 拧紧次数限制
        /// 从配置文件读取
        /// </summary>
        public int TightenCountLimit { get; set; } = 2; // 无限制

        /// <summary>
        /// 当前拧紧的螺丝序号
        /// </summary>
        int CurrentBoltIndex { get; set; } = 1;

        /// <summary>
        /// 网络状态变化事件
        /// </summary>
        public Action<int, bool> NetChangedAction { get; set; }

        /// <summary>
        /// 拧紧机向程序输出的拧紧结果
        /// </summary>
        public Action<int, TightenData> OnLastTightenAction { get; set; }

        /// <summary>
        /// 当前拧紧机需要拧紧的螺丝数量
        /// </summary>
        public Action<int, int> OnNeedTightenBoltCountAction { get; set; }

        /// <summary>
        /// 拧紧工作结束事件
        /// </summary>
        public Action<int, bool, TightenData> OnWorkUnitFinishedAction { get; set; }
        public int CurrentPset { get; private set; }
        public StationInfo StationInfo { get; }

        /// <summary>
        /// 拧紧机TCP客户端向外发送的条码信息
        /// </summary>
        public event OnVinNumberChangedDelegate VinNumberChangedEvent;

        public MyTightenManager(TightenConfig config, StationInfo stationInfo)
        {
            Debug.WriteLine($"MyTightenController Ctor()");
            Debug.WriteLine($"MyTightenController TightenConfig{config.Host}:{config.Port}");
            tdConfig = config;
            StationInfo = stationInfo;
            TightenDatas = new TightenDataCaChe();

            //Controller初始化时,就取库里读取本站、本枪对应的发动机特征码
            //TODO:考虑TAP并行
            //newTightenConfigCacahList = RFIDDBHelper.MSSQLHandler.TightenConfig.GetNewTightenConfigsByStationId(stationInfo.StationID, config.ToolId);
        }

        public int GetToolsID()
        {
            return tdConfig.ToolId;
        }

        public void Close()
        {
            TightenToolClient.Close();
            TightenToolClient = null;
        }

        public bool GetResult()
        {
            return TightenDatas.IsTightenOK();
        }

        /// <summary>
        /// 启动拧紧模块
        /// </summary>
        /// <returns></returns>
        public async Task<bool> OpenAsync()
        {
            try
            {
                Debug.WriteLine($"MyTightenController OpenAsync ip:{tdConfig.Host}  port:{tdConfig.Port}");
                
                Log.Information("启动拧紧机{Host}异步连接。", tdConfig.Host);
                return await Task.Run(() => Connect());
            }
            catch (Exception ex)
            {
                Log.Error("拧紧通讯初始化失败", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 建立通讯
        /// 调用同步方法连接时,会报MianForm已经Disposed的异常
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                if (TightenToolClient == null)
                {
                    AE2DeviceFactory fac = new AE2DeviceFactory();
                    TightenToolClient = fac.CreateTightenDevice(tdConfig);
                    TightenToolClient.NetChangedAction = TdNetChanged;
                    TightenToolClient.OnLastTightenData = ReadLastTightenData;
                    TightenToolClient.OnVehicleNumberAction = ReadVinNumber;
                }
                var start = DateTime.Now;
                
                if (TightenToolClient.Connect())
                {
                    NetStatus = true;
                    //Subscribe();
                }
                var cost =  DateTime.Now - start;
                int connectCostTime = cost.Seconds + 2;
                Debug.WriteLine($"拧紧连接耗时{connectCostTime - 2}s");
                Log.Information($"拧紧机{tdConfig.Host}通讯连接{(NetStatus ? "成功" : "失败")}。");
                return NetStatus;
            }
            catch (Exception ex)
            {
                Log.Error("拧紧通讯初始化失败", ex);
                NetChangedAction?.Invoke(tdConfig.ToolId, false);
                return false;
            }
        }

        /// <summary>
        /// 订阅事件
        /// 这里针对拧紧机的事件注册比较特殊
        /// </summary>
        private void Subscribe()
        {
            try
            {
                if (TightenToolClient == null)
                    return;

                if (TightenToolClient.SubcribeLastTightenData())
                {
                    Log.Information($"{tdConfig.NickName} ({tdConfig.Host})拧紧数据订阅成功。");
                }
                else
                {
                    Log.Information($"{tdConfig.NickName} ({tdConfig.Host})拧紧数据订阅失败。");
                }
                if (TightenToolClient.SubscribeVehicleNumber())
                {
                    Log.Information($"{tdConfig.NickName} ({tdConfig.Host})拧紧条码订阅成功。");
                }
                else
                {
                    Log.Information($"{tdConfig.NickName} ({tdConfig.Host})拧紧条码订阅失败。");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{tdConfig.NickName} [{tdConfig.Host}]拧紧机订阅消息异常", ex);
            }
        }

        /// <summary>
        /// 网络连接状态改变
        /// </summary>
        /// <param name="state"></param>
        private void TdNetChanged(bool state)
        {
            Debug.WriteLine($"MyTightenContrller -> TdNetChanged -> state : {state}");

            if (state)
            {
                //连接成功后注册事件
                Log.Information($"拧紧机已连接，{tdConfig.NickName} ({tdConfig.Host}:{tdConfig.Port})");
            }
            try
            {
                //触发外界订阅的事件
                NetChangedAction?.Invoke(tdConfig.ToolId, state);
            }
            catch (Exception ex)
            {
                Log.Error($"拧紧机{tdConfig.NickName} ({tdConfig.Host})在处理连接委托时异常。", ex);
            }
        }

        /// <summary>
        /// 读取拧紧数据
        /// 这应该是TightenClient的回调方法
        /// </summary>
        /// <param name="msg"></param>
        private void ReadLastTightenData(TightenData result)
        {
            Debug.WriteLine($"MyController -> ReadLastTightenData ->  {result.Torque}");
            Debug.WriteLine($"MyController -> ReadLastTightenData ->  {result.Pset}");

            try
            {
                if (result == null || result.Torque == 0)
                {
                    return;
                }
                if (string.IsNullOrEmpty(CurrentEngineCode) == false)
                    result.EngineCode = CurrentEngineCode;

                TightenDatas.AddTightenData(result);
                result.JobResult = GetResult() ? 1 : 0;

                //手动给螺栓号赋值
                result.BoltNo = CurrentBoltIndex;
                if(CurrentBoltIndex == 0)
                {
                    OnLastTightenAction?.Invoke(tdConfig.ToolId, result);
                    return;
                }
                if (result.Result == 0) //NG
                {
                    TightenCount++;
                    Debug.WriteLine($"MyController -> ReadLastTightenData -> CurrentBoltIndex = {CurrentBoltIndex}");
                    Debug.WriteLine($"MyController -> ReadLastTightenData -> TightenCount = {TightenCount}");

                    if (TightenCount >= TightenCountLimit && TightenCountLimit != 0) //NG
                    {
                        //锁枪
                        //TODO:测试时注释
                        TightenToolClient.DisableTightenTool();
                        //调用结束事件
                        OnWorkUnitFinishedAction?.Invoke(tdConfig.ToolId, false, result);
                    }
                    else
                    {
                        //调用拧紧结果事件
                        OnLastTightenAction?.Invoke(tdConfig.ToolId, result);
                    }
                }
                else
                {                    
                    Debug.WriteLine($"MyController -> ReadLastTightenData -> CurrentBoltIndex = {CurrentBoltIndex}");
                    Debug.WriteLine($"MyController -> ReadLastTightenData -> TightenCount = {TightenCount}");
                    Debug.WriteLine($"MyController -> ReadLastTightenData -> newTightenConfig ==null? {newTightenConfig == null}");

                    //当前螺丝处理完再加1
                    if (CurrentBoltIndex == newTightenConfig.TightenPointNum)
                    {
                        //调用结束事件
                        OnWorkUnitFinishedAction?.Invoke(tdConfig.ToolId, true, result);
                        //锁枪
                        //TODO:测试时注释
                        TightenToolClient.DisableTightenTool();
                    }
                    else
                    {
                        CurrentBoltIndex++;
                        //调用拧紧结果事件
                        OnLastTightenAction?.Invoke(tdConfig.ToolId, result);
                        GotoNextBolt(CurrentBoltIndex);
                    }
                    //到下一颗螺丝时，拧紧次数清0
                    TightenCount = 0;
                }

            }
            catch (Exception ex)
            {
                Log.Error("ReadLastTightenData", ex);
            }
        }

        /// <summary>
        /// 拧紧机TCP客户端向外发送的条码信息
        /// </summary>
        /// <param name="code"></param>
        private void ReadVinNumber(string code)
        {
            try
            {
                Log.Information("接收到拧紧机条码：{code}",code);
                if (code.Length > 17)
                {
                    code = code.Substring(0, 17);
                }
                if (code == CurrentEngineCode)
                {
                    VinNumberChangedEvent?.Invoke(tdConfig.ToolId, code);
                    //newTightenConfig中的信息
                    if (newTightenConfig != null && newTightenConfig.TightenPointNum > 0)
                    {
                        if (newTightenConfig.JobId.HasValue)
                        {
                            TightenToolClient.SelectJob(newTightenConfig.JobId.Value);
                        }
                        else if (newTightenConfig.BoltPset1.HasValue)
                        {
                            //设置Pset值，等待拧紧
                            GotoNextBolt(CurrentBoltIndex);
                        }
                        TightenToolClient.EnableTightenTool();
                    }
                    else
                    {
                        OnWorkUnitFinishedAction?.Invoke(tdConfig.ToolId, true, null);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "拧紧机条码接收处理报错");
            }
        }

        /// <summary>
        /// 设置拧紧点信息
        /// </summary>
        /// <param name="engineCode"></param>
        public void InitTightenInfo(string engineCode, LTightenConfigModel tConfig)
        {
            try
            {
                TightenCount = 0;
                CurrentBoltIndex = 0;
                Debug.WriteLine($"MyController -> InitTightenInfo -> CurrentBoltIndex = {CurrentBoltIndex}");
                Debug.WriteLine($"MyController -> InitTightenInfo -> TightenCount = {TightenCount}");
                if (string.IsNullOrEmpty(engineCode) && tConfig == null)
                {
                    newTightenConfig = null;
                    TightenDatas.ReSetTighten(0);
                    return;
                }
                newTightenConfig = tConfig;
                if (engineCode != CurrentEngineCode)
                {
                    //TODO:测试时注释
                    //TightenToolClient.DisableTightenTool();
                    CurrentEngineCode = engineCode;

                    //取发动机码前7位,到DB中查询相关信息
                    var queryEngineCode = engineCode.Substring(0, 7);
                    if(newTightenConfig == null)
                    {
                        newTightenConfig = new LTightenConfigModel
                        {
                            StationId = CurrentStationId,
                            TightenPointNum = 0
                        };
                    }
                    
                    //未配置拧紧信息
                    if (newTightenConfig.TightenPointNum == 0)
                    {
                        Debug.WriteLine($"当前工位未配置{engineCode}类型的拧紧信息");
                        Log.Information("当前工位未配置{engineCode}类型的拧紧信息", engineCode);
                        TightenDatas.ReSetTighten(0);
                        OnWorkUnitFinishedAction?.Invoke(tdConfig.ToolId, true, null);
                        return;
                    }
                    CurrentPointNum = 1;
                    //兼容
                    TightenDatas.ReSetTighten(newTightenConfig.TightenPointNum);
                    SetTighenCode(engineCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"设置拧紧信息出错{ex.Message}");
                Log.Error("设置拧紧信息出错", ex);
            }
        }

        /// <summary>
        /// 发送发动机条码到拧紧机
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool SetTighenCode(string code)
        {
            if (tdConfig.CodeRequest && !string.IsNullOrEmpty(code) && code.Length < 25)
            {
                try
                {
                    if (TightenToolClient?.SetVehicleNumber(code) == true)
                    {
                        Log.Information($"发送发动机条码{code}到拧紧机{tdConfig.NickName} [{tdConfig.Host}]成功");
                        return true;
                    }
                    else
                        Log.Information($"发送发动机条码{code}到拧紧机{tdConfig.NickName} [{tdConfig.Host}]失败");
                }
                catch (Exception ex)
                {
                    Log.Error("拧紧程序处理发动机码出错", ex);
                }
            }
            return false;
        }

        /// <summary>
        /// 查找当前拧紧枪螺栓数
        /// </summary>
        /// <param name="EngineCode"></param>
        /// <returns></returns>
        public int GetBoltCount()
        {
            return newTightenConfig == null ? 0 : newTightenConfig.TightenPointNum;
            //CurrentCode = EngineCode;
            ////TODO:数据中心取值
            //var tdcfg = RFIDDBHelper.MSSQLHandler.TightenConfig.GetNewTightenConfigsByStationId(stationId, EngineCode, tdConfig.ToolId)
            //    .OrderBy(p => p.ToolId).ToList();
            //if (tdcfg != null)
            //{
            //    CurrentPointNum = tdcfg.Sum(p => p.TightenPointNum);
            //}
            //else
            //{
            //    CurrentPointNum = 0;
            //    //TODO:测试时注释
            //    //TightenToolClient.DisableTightenTool();
            //}
        }

        /// <summary>
        /// 切换下一颗螺丝的Pset
        /// </summary>
        /// <param name="boltIndex"></param>
        /// <returns></returns>
        private bool GotoNextBolt(int boltIndex)
        {
            Debug.WriteLine($"InitTightenInfo-> GotoNextBolt({boltIndex})");
            if (newTightenConfig == null)
                return false;
            var rst = false;
            switch (boltIndex)
            {
                case 1:
                    if (newTightenConfig.BoltPset1 != null && CurrentPset != newTightenConfig.BoltPset1.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset1.Value);
                        CurrentPset = newTightenConfig.BoltPset1.Value;
                    }
                    break;
                case 2:
                    if (newTightenConfig.BoltPset2 != null && CurrentPset != newTightenConfig.BoltPset2.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset2.Value);
                        CurrentPset = newTightenConfig.BoltPset2.Value;
                    }
                    break;
                case 3:
                    if (newTightenConfig.BoltPset3 != null && CurrentPset != newTightenConfig.BoltPset3.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset3.Value);
                        CurrentPset = newTightenConfig.BoltPset3.Value;
                    }
                    break;
                case 4:
                    if (newTightenConfig.BoltPset4 != null && CurrentPset != newTightenConfig.BoltPset4.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset4.Value);
                        CurrentPset = newTightenConfig.BoltPset4.Value;
                    }
                    break;
                case 5:
                    if (newTightenConfig.BoltPset5 != null && CurrentPset != newTightenConfig.BoltPset5.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset5.Value);
                        CurrentPset = newTightenConfig.BoltPset5.Value;
                    }
                    break;
                case 6:
                    if (newTightenConfig.BoltPset6 != null && CurrentPset != newTightenConfig.BoltPset6.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset6.Value);
                        CurrentPset = newTightenConfig.BoltPset6.Value;
                    }
                    break;
                case 7:
                    if (newTightenConfig.BoltPset7 != null && CurrentPset != newTightenConfig.BoltPset7.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset7.Value);
                        CurrentPset = newTightenConfig.BoltPset7.Value;
                    }
                    break;
                case 8:
                    if (newTightenConfig.BoltPset8 != null && CurrentPset != newTightenConfig.BoltPset8.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset8.Value);
                        CurrentPset = newTightenConfig.BoltPset8.Value;
                    }
                    break;
                case 9:
                    if (newTightenConfig.BoltPset9 != null && CurrentPset != newTightenConfig.BoltPset9.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset9.Value);
                        CurrentPset = newTightenConfig.BoltPset9.Value;
                    }
                    break;
                case 10:
                    if (newTightenConfig.BoltPset10 != null && CurrentPset != newTightenConfig.BoltPset10.Value)
                    {
                        TightenToolClient.SetPSet(newTightenConfig.BoltPset10.Value);
                        CurrentPset = newTightenConfig.BoltPset10.Value;
                    }
                    break;
                default:
                    break;
            }
            return rst;
        }

        public bool EnableTool(bool status)
        {
            if (status)
            {
                return TightenToolClient.EnableTightenTool();
            }
            else
            {
                return TightenToolClient.DisableTightenTool();
            }
        }
    }
}
