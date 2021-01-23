using Advantech.Adam;
using Serilog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AE2Devices
{
    internal class AdamController : IAdamController
    {
        private AdamClient adamClient;
        #region 记录实时信号
        private bool[] diSignals = null;

        public bool NetStatus { get; set; }

        public event SensorTriggerDelegate OnSensorTrigger;
        public Action<IDevice,bool> NetChangedAction { get; set; }
        #endregion
        public AdamController(int port)
        {
            adamClient = new AdamClient(Adam4000Type.Adam4055, port);
        }
        
        public void Close()
        {
            if(adamClient != null)
            {
                adamClient.Close();
            }
        }

        public bool Open()
        {
           if(adamClient != null)
            {
                try
                {
                    adamClient.Open();
                }
                catch (Exception ex)
                {
                    Log.Error(ex,"ADAM连接异常.");
                    return false;
                }
                Log.Information("ADAM连接{Status}", adamClient.Opened ? "成功" : "失败");
                if (adamClient.Opened)
                {
                    new Thread(new ThreadStart(ReadDIO))
                    {
                        IsBackground = true
                    }.Start();
                    NetChangedAction?.BeginInvoke(this,true,null,null);
                    return true;
                }
            }
            return false;
        }
        public void OpenAsync()
        {
            Task.Run(() => Open());
        }
        /// <summary>
        /// 持续访问IO信号
        /// </summary>
        private void ReadDIO()
        {
            try
            {
                while (true)
                {
                    if (adamClient == null || adamClient.Opened == false)
                    {
                        NetChangedAction?.Invoke(this,false);
                        return;
                    }
                    if (adamClient.Get(out bool[] bDI, out bool[] bDO))
                    {
                        if (diSignals == null)
                        {
                            diSignals = bDI.Take(4).ToArray();
                            continue;
                        }
                        //ReadToStationSwitch(bDI[2]);
                        //ReadOutStationSwitch(bDI[1]);
                        ReadResetButton(bDI[0]);
                    }
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "[ReadDIO]{Message}",ex.Message);
                adamClient?.Close();
                NetChangedAction?.Invoke(this,false);
            }
        }
        /// <summary>
        /// 进工位开关信号处理
        /// </summary>
        /// <param name="signal"></param>
        private void ReadToStationSwitch(bool signal)
        {
            try
            {
                if (diSignals == null || diSignals[2] == signal)
                    return;
                diSignals[2] = signal;
                if (signal)//进工位
                {
                    OnSensorTrigger?.Invoke(EnumSensor.ToStation);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "读取进位开关信号异常");
            }
        }
        /// <summary>
        /// 出工位开关信号处理
        /// </summary>
        /// <param name="signal"></param>
        private void ReadOutStationSwitch(bool signal)
        {
            try
            {
                if (diSignals == null || diSignals[1] == signal)
                    return;
                diSignals[1] = signal;
                if (signal)
                {
                    OnSensorTrigger?.Invoke(EnumSensor.OutStation);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "读取出位开关信号异常");
            }
        }
        /// <summary>
        /// 读取复位按钮
        /// </summary>
        /// <param name="signal"></param>
        private void ReadResetButton(bool signal)
        {
            try
            {
                if (diSignals == null || diSignals[0] == signal)
                    return;
                diSignals[0] = signal;
                if (signal)
                {
                    AlarmWarning(false);
                    OnSensorTrigger?.Invoke(EnumSensor.Reset);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "读取复位开关信号异常");
            }
        }

        private void ReadAutoMatic(bool signal)
        {
            try
            {
                if (diSignals[0] == signal)
                    return;
                diSignals[0] = signal;
                if (signal)
                {
                    OnSensorTrigger?.Invoke(EnumSensor.AutoMatic);
                }
                else
                {
                    OnSensorTrigger?.Invoke(EnumSensor.Manual);
                }
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "读取复位开关信号异常");
            }
        }

        /// <summary>
        /// 异常报警
        /// </summary>
        /// <param name="state"></param>
        public bool AlarmWarning(bool state)
        {
            //if (!state)//暂时屏蔽报警,正式调试要去掉这句
            Warn(state);//报警自带蜂鸣效果
            return Beer(state);//这个蜂鸣太吵了
        }

        /// <summary>
        /// 报警
        /// </summary>
        /// <param name="state"></param>
        public bool Warn(bool state)
        {
            if (adamClient != null && adamClient.Opened)
                return adamClient.Set(0, state);
            else
                return false;
        }
        /// <summary>
        /// 蜂鸣
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool Beer(bool state)
        {
            return true;
            if (adamClient != null && adamClient.Opened)
                return adamClient.Set(1, state);
            else
                return false;
        }
    }
}
