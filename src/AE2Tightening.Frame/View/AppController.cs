using AE2Tightening.Configura;
using AE2Devices;
using AE2Tightening.Frame.Data;
using AE2Tightening.Frame.ViewModel;
using AE2Tightening.Lite;
using AE2Tightening.Lite.Model;
using AE2Tightening.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;

namespace AE2Tightening.Frame
{
    public class AppController : ApplicationContext
    {
        private IRFIDController rfid = null;
        /// <summary>
        /// IO模块
        /// IO模块是工控机上带的功能,所以程序里只有一个实例
        /// </summary>
        private IAdamController adam = null;

        private IOpcController opc = null;

        private IScanController scan = null;

        private ICardController mwCard = null;

        private readonly AppConfig appConfig;

        /// <summary>
        /// 窗体集合
        /// 这里是多屏设计,每个屏幕显示一个窗体的内容.每个窗体由一个ViewModel支撑
        /// </summary>
        private List<MainForm> forms = new List<MainForm>();

        /// <summary>
        /// 窗体ViewModel
        /// </summary>
        private List<MainViewModels> ViewModels = new List<MainViewModels>(2);

        private EngineViewModel currentEngine = null;

        private TagData currentTag = null;
        private Regex codeRegex = null;
        private string lastCode = "0";

        public AppController()
        {
            try
            {
                Log.Information("--------------系统启动------------");
                appConfig = Configs.ReadJsonConfig("Config.json");
                if (appConfig == null)
                {
                    MessageBox.Show("程序配置加载失败，详细请查看日志。", "系统异常");
                    ExitThread();
                    return;
                }
                Log.Information("json配置文件已加载");
                SubDeviceInitial();
                RunForms();
                Task.Run(() => StartSubDevice());
                //上传本地数据到服务器
                Thread upLoadThread = new Thread(new ThreadStart(UpLoadTightenData))
                {
                    IsBackground = true
                };
                upLoadThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "程序启动异常");
                Log.Error(ex, "初始化报错");
                throw ex;
            }
        }

        #region 外设初始化
        /// <summary>
        /// 启用连接外部设备
        /// </summary>
        /// <param name="_cfg"></param>
        private void SubDeviceInitial()
        {
            try
            {
                AE2DeviceFactory fac = new AE2DeviceFactory();
                //IO模块
                adam = fac.CreateAdamDevice(appConfig.Adam.PortNumber);
                adam.NetChangedAction = (d, n) =>
                {
                    ShowToAllForms(m => m.ADAMNet = n);
                };
                adam.OnSensorTrigger += Adam_OnSensorTrigger;

                //RFID
                rfid = fac.CreateRFIDDevice(appConfig.Rfid);
                //相当于连接变化时的事件
                rfid.NetChangedAction = (d, n) =>
                {
                    ShowToAllForms(m =>
                    {
                        m.RFIDNet = n;
                        m.PrintlnInfo("RFID已连接。");
                    });
                };
                rfid.OnTagDataReaded = TagReaded;//相当于RFID读到标签后的事件
                
                //串口条码枪
                scan = fac.CreateScanDevice(appConfig.Scanner);
                scan.OnScanCoded += Scan_OnScanCoded;

                //OPC
                opc = fac.CreateOpcDevice(appConfig.Opc);
                opc.LineStopChangedAction = (s) =>
                {
                    ShowToAllForms(v => v.StopLine = s);
                };
                opc.NetChangedAction = (d, s) =>
                {
                    ShowToAllForms(m =>
                    {
                        m.PLCNet = s;
                    });
                };
                opc.ShieldChangedAction = s =>
                {
                    ShowToAllForms(m =>
                    {
                        m.ShieldStatus = !s;
                        m.PrintlnInfo(s ? "PLC屏蔽PC信号" : "PLC启动接收PC信号");
                    });
                };

                mwCard = fac.CreateCardDevice(appConfig.MwCard);
                Log.Information("设备初始化完成");
            }
            catch (Exception ex)
            {
                Log.Error("外设模块初始化出错," + ex.Message, ex);
            }
        }

        /// <summary>
        /// 启动外设通讯
        /// </summary>
        private void StartSubDevice()
        {
            string err = "";
            try
            {
                err = "ADAM模块加载异常,请检查ADAM配置是否正确.";
                if (appConfig.Adam.Available)
                {
                    adam?.Open();
                }
                err = "RFID模块加载异常,请检查RFID配置是否正确.";
                if (appConfig.Rfid.Available)
                {
                    rfid?.Open();
                }
                err = "OPC模块加载异常,请检查OPC配置是否正确.";
                if (appConfig.Opc.Available)
                {
                    opc?.Open();
                }
                err = "扫描模块加载异常,请检查扫描配置是否正确.";
                if (appConfig.Scanner.Available && scan?.Open() == true)
                {
                    ShowToAllForms(m => m.ScantoolNet = true);
                }
                err = "刷卡器模块加载异常，请检查刷卡器配置是否正确";
                if(appConfig.MwCard.Available && mwCard.Open() == true)
                {
                    
                }
                bool status = opc?.GetShieldValue() ?? false;
                ViewModels.ForEach(m =>
                {
                    m.ShieldStatus = !status;
                    m.PrintlnInfo(status ? "PLC屏蔽PC信号" : "PLC启动接收PC信号");
                });

            }
            catch (Exception ex)
            {
                ShowToAllForms(m => m.PrintlnWarning(err));
                Log.Error(err, ex);
            }
        }

        #endregion

        #region 信号接收
        /// <summary>
        /// ADAM输入信号
        /// </summary>
        /// <param name="sensor"></param>
        private void Adam_OnSensorTrigger(EnumSensor sensor)
        {
            Debug.WriteLine($"ADAM输入信号: {sensor}");
            Log.Information($"[开关触发]:{sensor}");
            switch (sensor)
            {
                case EnumSensor.ToStation://进位
                    if (currentTag == null)
                    {
                        adam.AlarmWarning(true);
                        ViewModels.FirstOrDefault().EngineCode = "请补充扫描发动机码";
                        Log.Warning("车辆进位检测到RFID漏读！");
                    }
                    else if (currentTag.EngineCode == appConfig.NullCode)
                    {
                        ViewModels.FirstOrDefault().EngineCode = "空吊具";
                        Log.Information("进入一台空吊具.");
                    }
                    break;
                case EnumSensor.OutStation://限位
                    if (currentTag != null)
                        lastCode = currentTag.EngineCode;
                    currentTag = null;
                    Task.Run(() => CheckTighenResult());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// rfid标签数据响应
        /// </summary>
        /// <param name="tag"></param>
        private void TagReaded(TagData tag)
        {
            try
            {
                if (tag.EngineCode == lastCode && lastCode != appConfig.NullCode)
                {
                    if(currentTag == null)
                    {
                        adam.AlarmWarning(true);
                        ViewModels.ForEach(m => m.PrintlnWarning("RFID条码重复"));
                    }
                    return;
                }
                adam.AlarmWarning(false);
                currentTag = tag.Clone();
                //将条码发送到拧紧岗位
                OpenTightening(tag);

                RFIDDBHelper.MSSQLHandler.RFIDPointInfo.Insert(new RFIDPointInfoModel
                {
                    EngineCode = tag.EngineCode,
                    VNo = tag.SpreaderNo,
                    DeviceId = appConfig.Rfid.DeviceId
                });
            }
            catch (Exception ex)
            {
                Log.Error("读取RFID标签条码出现异常", ex);
            }
        }

        /// <summary>
        /// 扫描枪
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codeType"></param>
        private void Scan_OnScanCoded(string code)
        {
            Log.Information("扫描枪扫描条码：{code}", code);
            if (codeRegex == null)
            {
                codeRegex = new Regex(appConfig.BarCodePattern);
            }
            if (codeRegex.IsMatch(code))
            {
                EngineViewModel model = GetEngineViewModel(code, "");
                lock (forms)
                {
                    forms.ForEach(f => f.ReadScanCode(model));
                }
            }
            else
            {
                Log.Warning($"扫描的条码{code}格式不正确。Pattern:{appConfig.BarCodePattern}");
            }
        }

        /// <summary>
        /// 读OPC信号
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        private void ReadOpcData(string item, string value)
        {
            if (item == "ReadShieldSystem")
            {
                if (bool.TryParse(value, out bool status))
                {
                    ViewModels.ForEach(m =>
                    {
                        m.ShieldStatus = !status;
                        m.PrintlnInfo(status ? "PLC屏蔽PC信号" : "PLC启动接收PC信号");
                    });
                }
            }
            else if (item == "NoPass")
            {
                if (bool.TryParse(value, out bool status))
                {
                    ViewModels.ForEach(m =>
                    {
                        m.PrintlnInfo(status ? "已停线" : "已开线");
                    });
                }
            }
        }
        #endregion

        private EngineViewModel GetEngineViewModel(string code, string mto)
        {
            EngineViewModel model = null;
            try
            {
                if (!string.IsNullOrEmpty(code) && code != appConfig.NullCode)
                {
                    model = RFIDDBHelper.GetEngineOther(code, mto);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "获取机型信息报错！");
                ViewModels[0].PrintlnWarning(ex.Message);
            }
            if (model == null)
                model = EngineViewModel.CreateModel(code, mto);
            return model;
        }
        /// <summary>
        /// 将条码发送到拧紧岗位
        /// </summary>
        /// <param name="code"></param>
        private void OpenTightening(TagData tag)
        {
            try
            {
                if (tag == null || string.IsNullOrEmpty(tag.EngineCode))
                    return;
                EngineViewModel model = GetEngineViewModel(tag.EngineCode, tag.EngineMto);
                currentEngine = model;
                lock (forms)
                    forms.ForEach(f => f.SetEngineCode(model?.Clone()));
            }
            catch (Exception ex)
            {
                Log.Error("在启动终端拧紧时出现异常", ex);
            }
        }

        /// <summary>
        /// 检查拧紧结果
        /// </summary>
        private void CheckTighenResult()
        {
            try
            {
                if (currentEngine == null || currentEngine.EngineCode == Configs.FileConfigs.NullCode)
                    return;
                if (forms.Any(f => f.GetTdResult() == false))
                {
                    if (currentEngine != null && Configs.FileConfigs.IgnoreEngine.Any(c => currentEngine.EngineCode.StartsWith(c)))
                    {
                        Log.Information("当前机型NG不停线:{code}",currentEngine.EngineCode);
                        adam.AlarmWarning(true);
                        ViewModels.ForEach(v => v.PrintlnInfo("当前机型NG不停线"));
                        return;
                    }
                    
                   Log.Warning("拧紧不合格");
                   AlarmAndStopLine(true);
                }
                else if (forms.Any(f => f.GetPartResult() == false))
                {
                    Log.Warning($"零件绑定不匹配，停线报警");
                    AlarmAndStopLine(true);
                }
               
            }
            catch (Exception ex)
            {
                Log.Error("出工位获取作业结果时报错", ex);
            }
        }

        /// <summary>
        /// 分屏启动界面
        /// </summary>
        private void RunForms()
        {
            try
            {
                Screen[] scs = Screen.AllScreens;
                int left = 0;
                for (int i = 0; i < scs.Length; i++)
                {
                    if (appConfig.Screenlist.Length > i)
                    {
                        ScreenConfig fConfig = appConfig.Screenlist[i];
                        MainViewModels viewmodel = new MainViewModels();
                        MainForm f = new MainForm(fConfig, viewmodel, adam, opc, mwCard)
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new System.Drawing.Point(left + 10, scs[i].Bounds.Top)
                        };
                        left += scs[i].Bounds.Right;
                        f.Show();
                        forms.Add(f);
                        ViewModels.Add(viewmodel);
                        if (i == 0)
                        {
                            MainForm = f;
                        }
                        viewmodel.OnWorkCompate = WorkComplate;
                        Log.Information("启动窗体：{Title}", appConfig.Screenlist[i].Title);
                    }
                }
                Log.Information("窗口加载完成");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "启动界面报错");
            }
        }

        /// <summary>
        /// 作业完成时处理
        /// </summary>
        private void WorkComplate()
        {
            //作业合格时，自动开线
            if (forms.All(f => f.GetTdResult() && f.GetPartResult()) )
            {
                AlarmAndStopLine(false);
            }
        }

        /// <summary>
        /// 报警停线
        /// </summary>
        /// <param name="state"></param>
        public void AlarmAndStopLine(bool state)
        {
            try
            {
                if (adam != null)
                {
                    adam.AlarmWarning(state);//报警
                }
                if (state)
                {
                    opc?.NoPass(true);//停线
                    Log.Warning("AlarmAndStopLine停线");
                }
                else
                {
                    opc?.NoPass(false);//放行
                    Log.Information("AlarmAndStopLine放行");
                }
            }
            catch (Exception ex)
            {
                Log.Error("调用alam报警异常。", ex);
            }
        }

      

        private void ShowToAllForms(Action<MainViewModels> action)
        {
            ViewModels.ForEach(action);
        }

        /// <summary>
        /// 拧紧数据上传
        /// </summary>
        private async void UpLoadTightenData()
        {
            while (true)
            {
                try
                {
                    List<TightenModel> lstData = await RFIDDBHelper.LocalSQLHandler.TighteningService.GetNoUploadDataAsync(10);
                    if (lstData.Count > 0)
                    {
                        foreach (var data in lstData)
                        {
                            TighteningResultModel model = TightenMapper.MapTightenData(data);
                            if (RFIDDBHelper.MSSQLHandler.TightenService.Insert(model))
                            {
                                data.IsUpload = 1;
                                await RFIDDBHelper.LocalSQLHandler.TighteningService.UpdateAsync(data);
                            }
                        }
                    }

                    List<LTopEngineResultModel> lstStationData = await RFIDDBHelper.LocalSQLHandler.LTopEngineResult.GetNoUploadDataAsync(10);
                    if (lstStationData.Count > 0)
                    {
                        foreach (var item in lstStationData)
                        {
                            if (currentEngine != null && item.EngineCode == currentEngine.EngineCode)
                            {
                                continue;
                            }
                            TopEngineResultModel model = new TopEngineResultModel()
                            {
                                EngineCode = item.EngineCode,
                                Result = item.Result,
                                StationId = item.StationId,
                                CreateTime = item.CreateTime,
                                UpdateTime = item.UpdateTime
                            };
                            if (RFIDDBHelper.MSSQLHandler.TopEngineResult.Insert(model) > 0)
                            {
                                item.IsUpload = 1;
                                await RFIDDBHelper.LocalSQLHandler.LTopEngineResult.UpdateAsync(item);
                            }
                        }
                    }
                }
                catch (Exception exx)
                {
                    Log.Error("上传拧紧数据时出现异常", exx);
                }
                finally
                {
                    await Task.Delay(1000 * 30);
                }
            }
        }

    }//cls
}//ns
