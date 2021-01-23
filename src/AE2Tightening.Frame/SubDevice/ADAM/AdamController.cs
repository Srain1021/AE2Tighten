using Advantech.Adam;
using AE2Tightening.Configura;
using GodSharp.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Frame
{
    public class AdamController : IAdamController
    {
        private readonly Configs _config;
        private readonly Logging _logger;
        private AdamClient adamClient;
        #region 记录实时信号
        private bool[] diSignals = null;

        public event SensorTriggerDelegate OnSensorTrigger;
        public Action<bool> OnNetChanged { get; set; }
        #endregion
        public AdamController(Configs config, Logging logger)
        {
            _config = config;
            _logger = logger;
            adamClient = new AdamClient(Adam4000Type.Adam4055, _config.FileConfigs.Adam.PortNumber);
        }
        
        public void Close()
        {
            if(adamClient != null)
            {
                adamClient.Close();
            }
        }

        public void Start()
        {
           if(adamClient != null)
            {
                try
                {
                    adamClient.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                _logger?.Info("ADAM连接" + (adamClient.Opened ? "成功" : "失败"));
                if (adamClient.Opened)
                {
                    Task.Run(() => ReadDIO());
                    OnNetChanged?.Invoke(true);
                }
            }
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
                        OnNetChanged?.Invoke(false);
                        return;
                    }
                    if (adamClient.Get(out bool[] bDI, out bool[] bDO))
                    {
                        if (diSignals == null)
                        {
                            diSignals = bDI.Take(4).ToArray();
                            continue;
                        }
                        ReadToStationSwitch(bDI[3]);
                        ReadOutStationSwitch(bDI[2]);
                        ReadResetButton(bDI[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.Error($"[ReadDIO]{ex.Message}", ex);
                adamClient?.Close();
                OnNetChanged?.Invoke(false);
            }
        }
        /// <summary>
        /// 进工位开关信号处理
        /// </summary>
        /// <param name="signal"></param>
        private void ReadToStationSwitch(bool signal)
        {
            if (diSignals == null || diSignals[3] == signal)
                return;
            diSignals[3] = signal;
            if (signal)//进工位
            {
                OnSensorTrigger?.Invoke(EnumSensor.ToStation);
                //if (AppInstance.HaveEngine == false)//检查RFID读写头是否异常
                //{
                //    //发动机码为空，标签到达限位开关时，没有读取到标签，或标签数据异常
                //    _logger.Log("到达进车位置时，RFID没有读取到可用标签信息", LoggingLevel.Warn);
                //    OnSensorTrigger?.BeginInvoke(EnumSensor.ToStation, null, null);
                //}
            }
        }
        /// <summary>
        /// 出工位开关信号处理
        /// </summary>
        /// <param name="signal"></param>
        private void ReadOutStationSwitch(bool signal)
        {
            if (diSignals == null || diSignals[2] == signal)
                return;
            diSignals[2] = signal;
            if (signal)
            {
                OnSensorTrigger?.Invoke(EnumSensor.OutStation);
            }
        }
        /// <summary>
        /// 读取复位按钮
        /// </summary>
        /// <param name="signal"></param>
        private void ReadResetButton(bool signal)
        {
            if (diSignals == null || diSignals[1] == signal)
                return;
            if(signal == false)
            {
                diSignals[1] = signal;
                return;
            }
            Reset();
        }

        /// <summary>
        /// 复位
        /// </summary>
        public void Reset()
        {
            AlarmWarning(false);//先消除报警
            //_opc.RunLine();//开线放行
        }

        /// <summary>
        /// 异常报警
        /// </summary>
        /// <param name="state"></param>
        public void AlarmWarning(bool state)
        {
            if (state)
                return;
            Warn(state);//报警自带蜂鸣效果
            //Beer(state);//这个蜂鸣太吵了
        }

        /// <summary>
        /// 报警
        /// </summary>
        /// <param name="state"></param>
        private bool Warn(bool state)
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
        private bool Beer(bool state)
        {
            if (adamClient != null && adamClient.Opened)
                return adamClient.Set(1, state);
            else
                return false;
        }


        public void TestToStation(bool state)
        {
            ReadToStationSwitch(state);
        }

        public void TestOutStation(bool state)
        {
            ReadOutStationSwitch(state);
        }
    }
}
