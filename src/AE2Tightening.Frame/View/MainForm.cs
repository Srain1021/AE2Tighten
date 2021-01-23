using AE2Devices;
using AE2Tightening.Configura;
using AE2Tightening.Frame.Data;
using AE2Tightening.Frame.ViewModel;
using AE2Tightening.Lite.Model;
using AE2Tightening.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AE2Tightening.Lite;
using System.Windows.Forms;

namespace AE2Tightening.Frame
{
    public partial class MainForm : Form
    {
        private Timer timer = new Timer() { Interval = 1000 };//计数器-用于显示日期
        //private readonly Logging Log = null;//日志组件
        #region 外部接口
        /// <summary>
        /// 拧紧枪集合
        /// </summary>
        private Dictionary<int,ITightenManager> tightenControllers = new Dictionary<int, ITightenManager>();
        private ITightenManager CurrentTool = null;
        /// <summary>
        /// IO模块
        /// IO模块是工控机上带的,一般只会安装一个,所以只会有一个实例
        /// </summary>
        private IAdamController adam = null;
        private IOpcController opc = null;
        private ICardController mwCard = null;
        public Action<bool, bool> AlarmAction;
        public Action<bool> ShieldChandeAction;
        #endregion
        #region 结果展示
        private DataTable tdDataTable = new DataTable();

        private MainViewModels ViewModel { get; }

        #endregion
        #region 数据缓存
        private Regex codeRegex = new Regex("^[A-Z][A-Z0-9]{7}[0-9]{4}");
        private readonly string theTitle = "";
        private EngineViewModel EngineInfo = null;
        private MaterialMetasModel metasModel = null;
        private StationModel Station = null;
        private string CurrentCode = "";
        private int CurrentPointNum = 0;
        private string lastMTO = "";
        //private TopEngineResultModel topEngineResultModel = null;
        private LTopEngineResultModel topEngineResultModel = null;
        private List<LTightenConfigModel> lTightenConfigs = null;
        private LTightenConfigModel curTightenCfg = null;
        private int theCfgTdCount = 0;
        #endregion

        //Action<EngineViewModel, bool> StartTighten;
        private FormAuthor frmAuthor = null;
        //配置信息
        private ScreenConfig _screenConfig = null;

        public MainForm(ScreenConfig scnConfig = null)
        {
            //InitializeComponent();
            //Load += FormForm_Load;
            _screenConfig = scnConfig;
            ViewModel = new MainViewModels();
        }

        public MainForm(ScreenConfig config, MainViewModels viewModel, IAdamController iAdam,IOpcController iopc,ICardController iCard)
        {
            try
            {
                _screenConfig = config;
                ViewModel = viewModel ?? new MainViewModels();
                adam = iAdam;
                opc = iopc;
                mwCard = iCard;
                InitializeComponent();
                Load += FormForm_Load;
                adam.OnSensorTrigger += Adam_OnSensorTrigger;//开关\按钮信息
                mwCard.SwipedEvent += MwCard_SwipedEvent;
                if (_screenConfig != null)
                {
                    lblAppName.Text = _screenConfig.Title;
                    if (_screenConfig.Part.Available)
                    {
                        panelPart.Visible = true;
                    }
                    if (config.Id != 0)
                    {
                        btClose.Visible = false;
                    }
                    theTitle = config.Title;
                }

                {
                    var dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                    dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                    dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
                    dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 36F);
                    dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
                    dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
                    dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                    dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
                    this.dataViewTd.DefaultCellStyle = dataGridViewCellStyle1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form:" + ex.Message);
            }
        }

    
        private void FormForm_Load(object sender, EventArgs e)
        {
            try
            {
                Timer_Tick(timer, null);
                WindowState = FormWindowState.Maximized;
                ControlBindDataSource();//视图模型绑定
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
                Task.Run(()=>InitializeTightenConfig());
                timer.Tick += Timer_Tick;//时钟显示
                timer.Start();
                codeRegex = new Regex(Configs.FileConfigs.BarCodePattern);
                
                dataViewTd.RowsAdded += (s, ee) =>
                {
                    try
                    {
                        Debug.WriteLine($"dataViewTd.RowsAdded -> {s}");
                        dataViewTd.Columns[0].Width = 200;

                        var dvt = (DataGridView)s;
                        var tightenstatus = (string)dvt.Rows[ee.RowIndex].Cells[4].Value;
                        if (tightenstatus == "NG")
                        {
                            dvt.Rows[ee.RowIndex].DefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.Red };
                        }
                        else
                        {
                            dvt.Rows[ee.RowIndex].DefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.Green };
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"dataViewTd.RowsAdded.Exception -> {ex.Message}");
                    }
                };
                
            }
            catch (Exception ex)
            {
                Log.Error($"[{theTitle}]软件初始化启动异常", ex);
                ViewModel.PrintlnWarning(ex.Message);
            }
        }

        /// <summary>
        /// 视图属性更新时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "TighteningData":
                        {
                            if (ViewModel.TighteningData == null)
                                tdDataTable.Rows.Clear();
                            else
                                tdDataTable.Rows.Add(
                                    ViewModel.TighteningData.BoltNo,
                                    ViewModel.TighteningData.Pset,
                                    ViewModel.TighteningData.Torque,
                                    ViewModel.TighteningData.Angle,
                                    ViewModel.TighteningData.Result == 1 ? "OK" : "NG");
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(e.PropertyName, ex);
            }
        }
       

        #region 初始化

        /// <summary>
        /// 拧紧机初始化(配置文件、数据库)
        /// </summary>
        private void InitializeTightenConfig()
        {
            string err = "";
            try
            {
                //初始化拧紧机
                #region 集合方式初始化
                if (_screenConfig.Tighten != null)
                {
                    for (int i = 0; i < _screenConfig.Tighten.Length; i++)
                    {
                        var tightenTool = new MyTightenManager(_screenConfig.Tighten[i],_screenConfig.Station)
                        {
                            NetChangedAction = (toolid,net) =>
                            {
                                if(toolid == 1)
                                {
                                    ViewModel.Tighten1Net = net;
                                }
                                else
                                {
                                    ViewModel.TightenNet2 = net;
                                }
                                if (net)
                                {
                                    ViewModel.PrintlnInfo($"拧紧机{_screenConfig.Tighten[i].NickName}已连接。");
                                }
                                else
                                {
                                    ViewModel.PrintlnInfo($"拧紧机{_screenConfig.Tighten[i].NickName}断开连接");
                                }

                            },
                            OnLastTightenAction = OnReadTightenData,
                            OnWorkUnitFinishedAction = TightenControllerFinished
                        };
                        tightenControllers.Add(_screenConfig.Tighten[0].ToolId, tightenTool);
                        if (!tightenTool.Connect())
                        {
                            ViewModel.PrintlnWarning("拧紧机通讯连接失败");
                        }
                        switch (i)
                        {
                            case 0:
                                Invoke(new Action(() => monitorTD1Net.Visible = true));
                                break;
                            case 1:
                                Invoke(new Action(() => monitorTD2Net.Visible = true));
                                break;
                        }
                    }
                }

                #endregion
                err = "Station数据获取异常";
                Station = RFIDDBHelper.MSSQLHandler.StationService.Get(_screenConfig.Station.StationID);
            }
            catch (Exception ex)
            {
                ViewModel.PrintlnWarning(err);
                Log.Error($"[{theTitle}]{err}", ex);
            }
        }

        /// <summary>
        /// 将控件与视图模型绑定
        /// </summary>
        private void ControlBindDataSource()
        {
            try
            {
                ViewModel.SynchronizeInvoker = this;
                #region 状态监控类控件绑定
                this.monitorScanNet.DataBindings.Add("Status", ViewModel, "ScantoolNet");
                this.monitorShield.DataBindings.Add("Status", ViewModel, "ShieldStatus");
                this.monitorRfidNet.DataBindings.Add("Status", ViewModel, "RFIDNet");
                this.monitorAdamNet.DataBindings.Add("Status", ViewModel, "ADAMNet");
                this.monitorTD1Net.DataBindings.Add("Status", ViewModel, nameof(ViewModel.Tighten1Net));
                this.monitorTD2Net.DataBindings.Add("Status", ViewModel, nameof(ViewModel.TightenNet2));
                this.monitorPLCNet.DataBindings.Add("Status", ViewModel, "PLCNet");
                this.monitorStopLine.DataBindings.Add("Status", ViewModel, nameof(ViewModel.Pass));
                #endregion
                #region 文本控件绑定
                lblEngineCode.DataBindings.Add("Text", ViewModel, "EngineCode");
                lblMTO.DataBindings.Add("Text", ViewModel, "EngineMTO");
                lblEngintType.DataBindings.Add("Text", ViewModel, "EngineType");
                lblBindCode.DataBindings.Add("Text", ViewModel, "PnoCode");
                lblBindStatus.DataBindings.Add("Text", ViewModel, "CodeBindStatus");
                lblResult.DataBindings.Add("Text", ViewModel, "TightenResult");
                lblBindStatus.DataBindings.Add("BackColor", ViewModel, "BindStatusColor");
                lblCurTorque.DataBindings.Add("Text", ViewModel, nameof(ViewModel.CurTorque));
                lblCurTorque.DataBindings.Add("ForeColor", ViewModel, nameof(ViewModel.TorqueColor));
                lblCurAngle.DataBindings.Add("Text", ViewModel, "CurAngle");
                #endregion
                #region 数据控件绑定
                tdDataTable.Columns.Add(new DataColumn() { ColumnName = "螺栓号" });
                tdDataTable.Columns.Add(new DataColumn() { ColumnName = "程序号" });
                tdDataTable.Columns.Add(new DataColumn() { ColumnName = "力矩" });
                tdDataTable.Columns.Add(new DataColumn() { ColumnName = "角度" });
                tdDataTable.Columns.Add(new DataColumn() { ColumnName = "结果" });
                dataViewTd.DataSource = tdDataTable;
                #endregion
                ViewModel.PrintlnInfo = PrintInfo;
                ViewModel.PrintlnWarning = PrintWarning;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintInfo(string log)
        {
            PrintView(log, Color.White);
        }
        private void PrintWarning(string log)
        {
            PrintView(log, Color.Red);
        }

        private void PrintView(string log, Color col)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, Color>(PrintView), new object[] { log, col });
            }
            else
            {
                log = log ?? "";
                var item = listViewLog.Items.Add(log);
                item.BackColor = col;
                if (col != Color.White)
                {
                    item.ForeColor = Color.White;
                }
                else
                {
                    item.ForeColor = Color.Black;
                }
                if (listViewLog.Items.Count > 200)
                {
                    listViewLog.Items.RemoveAt(0);
                }
                item.EnsureVisible();
            }
        }
        #endregion

        #region ADAM按钮
        /// <summary>
        /// 开关信号触发事件
        /// </summary>
        /// <param name="sensor"></param>
        private void Adam_OnSensorTrigger(EnumSensor sensor)
        {
            switch (sensor)
            {
                case EnumSensor.Reset:
                    if(_screenConfig.Id == 0)
                    {
                        if (!ViewModel.Pass)
                        {
                            Invoke(new Action(() =>
                            {
                                //刷卡
                                if (frmAuthor != null && frmAuthor.Visible)
                                {
                                    frmAuthor.Dispose();
                                }
                                frmAuthor = new FormAuthor(AuthorType.放行, card =>
                                {
                                    if (card != null)
                                    {
                                        opc.Pass();
                                        Task.Run(()=>EngineOutStation());
                                    }
                                });
                                frmAuthor.Show(this);
                            }));
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 出工位判定
        /// </summary>
        /// <param name="adam"></param>
        private void EngineOutStation()
        {
            try
            {
                if (EngineInfo == null || string.IsNullOrEmpty(EngineInfo.EngineCode) || EngineInfo.EngineCode == Configs.FileConfigs.NullCode)
                    return;
                bool result = true;
                if (GetTdResult() == false)
                {
                    result = false;
                    UpdateEngineResultToDB(EngineInfo.EngineCode, 0);
                }
                if (_screenConfig.Part.Bind)
                {
                    if (metasModel != null && metasModel.Id <= 0 && string.IsNullOrEmpty(metasModel.MaterialCode))
                    {
                        metasModel.MaterialCode = "";
                        metasModel.Result = 0;
                        Task.Run(() => RFIDDBHelper.MSSQLHandler.SaveMaterialMetas(metasModel));
                    }
                    if (metasModel == null || metasModel.Result != 1)
                    {
                        result = false;
                    }
                }
                if (!result)
                {
                    Task.Run(() => UpdateStationInfo(Station, 0));
                }
                else
                {
                    EngineInfo = null;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"[{theTitle}]EngineOutStation", ex);
            }

        }

        #endregion

        #region 刷卡器
        /// <summary>
        /// 刷卡
        /// </summary>
        /// <param name="obj"></param>
        private void MwCard_SwipedEvent(CardInfo obj)
        {
            try
            {
                CardAuthorModel cardModel = RFIDDBHelper.MSSQLHandler.CardAuthor.GetByNo(obj.CardId);
                if (cardModel == null)
                {
                    return;
                }
                var card = new UserInfo
                {
                    CardId = obj.CardId,
                    SwipeTime = obj.SwipeTime,
                    Name = cardModel.Name,
                    CanRunLine = cardModel.RunLine == 1,
                    CanSystemShield = cardModel.SystemShield == 1
                };
                Log.Information("刷卡授权：{id}-{name}-{canRunline}-{canShield}", card.CardId, card.Name, card.CanRunLine, card.CanSystemShield);
                if (frmAuthor != null && !frmAuthor.IsDisposed && frmAuthor.Visible)
                {
                    frmAuthor.SetCardInfo(card);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "刷卡异常");
                ViewModel.PrintlnWarning(ex.Message);
            }
        }
        #endregion

        #region 条码输入
        /// <summary>
        /// 拧紧机条码接收
        /// </summary>
        /// <param name="code"></param>
        private void Tighten_OnVinNumberChanged(string code)
        {
            //try
            //{
            //    //在减震器岗位靠从拧紧机获取零件码绑定到发动机
            //    if (_screenConfig.Part.Bind && _screenConfig.Tighten.CodeType == EnumCodeType.PartCode)
            //    {
            //        Log.Information($"拧紧机扫描条码：{code}");
            //        if (codeRegex.IsMatch(code))//拧紧机扫发动机码
            //        {
            //            SetEngineCode(code);
            //        }
            //        else
            //        {
            //            //条码绑定
            //            PartCodeBinding(code);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error($"Tighten_OnVinNumberChanged({code})", ex);
            //}
        }

        private void SetEngineCode(string code)
        {
            EngineViewModel engine = GetEngineCodeInfo(code);
            if (engine == null)
            {
                Log.Warning($"条码{code}异常，没有找到发动机信息");
                ViewModel.PrintlnWarning($"没有找到{code}的机型信息");
                engine = EngineViewModel.CreateModel(code);
            }
            ReadScanCode(engine);
        }

        /// <summary>
        /// 发动机码（RFID\扫描）
        /// </summary>
        /// <param name="code"></param>
        public void SetEngineCode(EngineViewModel engine)
        {
            if (engine == null)
            {
                Log.Warning("接收到发动机信息engine为null");
                DisPlayEngineInfo(null);
                return;
            }
            ViewModel.PrintlnInfo($"RFID:{engine.EngineCode} {engine.EngineName}");
            EngineInfo = engine.Clone();
            string engineCode = engine.EngineCode;
            string engineMto = engine.EngineMTO;
            try
            {
                if (engineCode == Configs.FileConfigs.NullCode || string.IsNullOrEmpty(engineCode))
                {
                    DisPlayEngineInfo(null);
                    //空吊具与空条码有不同的提示
                    if (!string.IsNullOrEmpty(engineCode))/*engineCode == 000000000000;这种是吊具上没有发动机，无需作业*/
                    {
                        ViewModel.PrintlnInfo("读取到空托盘标签");
                    }
                    ViewModel.TightenResult = "";
                    ViewModel.TighteningData = null;
                    ViewModel.CurTorque = 0;
                    ViewModel.CurAngle = "";
                    ViewModel.TorqueColor = Color.Black;
                }
                else
                {
                    ViewModel.CurTorque = 0;
                    ViewModel.CurAngle = "";
                    ViewModel.TorqueColor = Color.Black;
                    //StartTighten?.Invoke(engine, isAutoMatic);
                    StartTighten(engine);
                }
            }
            catch (Exception ex)
            {
                Log.Error("SetEngineCode:" + engineCode, ex);
                ViewModel.PrintlnWarning(ex.Message);
            }
        }
        /// <summary>
        /// 扫描条码开始拧紧
        /// </summary>
        /// <param name="engine"></param>
        public void ReadScanCode(EngineViewModel engine)
        {
            if (engine == null)
            {
                Log.Warning("扫描得到的engine为null");
                DisPlayEngineInfo(null);
                return;
            }
            EngineInfo = engine.Clone();
            ViewModel.PrintlnInfo($"扫码:{engine.EngineCode} {engine.EngineName}");

            StartTighten(engine);//扫描的条码不分手自动
        }


        #region 拧紧
        /// <summary>
        /// 开始拧紧作业
        /// 正儿八经的开始控制拧紧枪
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="isSendVIN">是否向TightenControl发送条码</param>
        private async void StartTighten(EngineViewModel engine)
        {
            /*
             * 不管手动自动扫描到条码后都会走到这里.
             * 所以向两个拧紧机发送条码的代码先添加到这里.
             */
            curTightenCfg = null;
            try
            {
                DisPlayEngineInfo(engine);
                ViewModel.TightenResult = "";
                ViewModel.TighteningData = null;
                CurrentCode = engine.EngineCode;
                await CreateResult(engine.EngineCode);
                if (string.IsNullOrEmpty(engine.EngineMTO))
                {
                    ViewModel.PrintlnWarning($"没有找到发动机{engine.EngineCode}派生");
                }
                lTightenConfigs = await RFIDDBHelper.LocalSQLHandler.LTightenConfig.GetTdConfig(engine.EngineCode, _screenConfig.Station.StationID);
                if (lTightenConfigs == null)
                {
                    lTightenConfigs = new List<LTightenConfigModel>();
                }
                theCfgTdCount = lTightenConfigs.Sum(s => s.TightenPointNum);
                Log.Information("拧紧配置数：{tcount},拧紧点数：{pcount}",lTightenConfigs.Count,theCfgTdCount);
                CurrentPointNum = 1;
                InitTightenTool();
                RunNextTightenTool();
                if (Station != null)
                {
                    Station.LastCode = engine.EngineCode;
                    RFIDDBHelper.MSSQLHandler.StationService.Update(Station);
                }
            }
            catch (Exception ex)
            {
                Log.Error("接收条码开始拧紧时出现错误", ex);
            }
        }

        private void InitTightenTool()
        {
            foreach(int tid in tightenControllers.Keys)
            {
                tightenControllers[tid].InitTightenInfo("", null);
            }
        }

        /// <summary>
        /// 运行下一个电枪拧紧
        /// </summary>
        private void RunNextTightenTool()
        {
            if (lTightenConfigs.Count > 0)
            {
                int index = 0;
                if (curTightenCfg != null)
                {
                    index = lTightenConfigs.IndexOf(curTightenCfg) + 1;
                }
                if (lTightenConfigs.Count > index)
                {
                    curTightenCfg = lTightenConfigs[index];
                    if (!tightenControllers.ContainsKey(curTightenCfg.ToolId))
                    {
                        ViewModel.PrintlnWarning($"拧紧参数配置异常，没有id为{curTightenCfg.ToolId}的电枪");
                        adam.AlarmWarning(true);
                        return;
                    }
                    Log.Information("电枪{name}需拧紧螺栓个数：{count}", curTightenCfg.ToolId,curTightenCfg.TightenPointNum);
                }
                else
                {
                    Log.Information("拧紧结束结算");
                    curTightenCfg = null;
                    //结算
                    TightenComplate();
                    return;
                }
                if (curTightenCfg!=null && tightenControllers[curTightenCfg.ToolId] != null)
                {
                    tightenControllers[curTightenCfg.ToolId].InitTightenInfo(CurrentCode, curTightenCfg);
                    //向拧紧机发送条码，通过isSendVIN参数第一步判断,tdconfig里面的配置是第二步
                    //tightenControllers[curTightenCfg.ToolId].SetTighenCode(CurrentCode);

                    if (_screenConfig.Part.Available)
                    {
                        ViewModel.PnoCode = $"请扫描{_screenConfig.Part.PartName}条码";
                        ViewModel.CodeBindStatus = "";
                    }
                }
                else
                {
                    adam.AlarmWarning(true);
                    ViewModel.PrintlnWarning($"ToolId为{curTightenCfg.ToolId}的电枪对象为空！");
                    Log.Warning("ToolId为{ToolId}的电枪对象为空！", curTightenCfg.ToolId);
                }
            }
            else
            {
                //结算
                TightenComplate();
                return;
            }
        }

        /// <summary>
        /// 拧紧结果结算
        /// </summary>
        private void TightenComplate()
        {
            if (GetTdResult())
            {
                UpdateEngineResultToDB(CurrentCode, 1);
                ViewModel.TightenResult = "OK";
                if (_screenConfig.Part.Bind)
                {
                    if (metasModel != null && metasModel.Result == 1)
                    {
                        UpdateStationInfo(Station, 1);
                    }
                    else
                        return;
                }
                //如停线，自动放行
                opc.Pass();
            }
            else
            {
                ViewModel.TightenResult = "NG";
                adam.AlarmWarning(true);
            }
        }

        /// <summary>
        /// 拧紧机作业完成事件
        /// 根据1号机完成的结果,确定是否开启2号机
        /// </summary>
        /// <param name="isOk"></param>
        /// <param name="tightenData"></param>
        private void TightenControllerFinished(int toolId, bool isOk, TightenData tightenData)
        {
            try
            {
                if (tightenData == null || curTightenCfg == null)
                {
                    //不需要拧紧
                    RunNextTightenTool();
                    return;
                }
                OnReadTightenData(toolId, tightenData);
                if (toolId != curTightenCfg.ToolId)
                {
                    adam.AlarmWarning(true);
                    ViewModel.PrintlnWarning("当前使用电枪与配置电枪不符！");
                    return;
                }
                if (isOk)//拧紧机完成作业的结果.全部OK时是true,有NG是false
                {
                    adam.AlarmWarning(false);
                    RunNextTightenTool();
                }
                else
                {
                    //连续NG报警
                    ViewModel.PrintlnWarning("连续NG超过限制，锁枪！");
                    adam.AlarmWarning(true);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "处理电枪结果回调时报错!");
            }
        }
        #endregion


        /// <summary>
        /// 新增结果记录
        /// </summary>
        /// <param name="code"></param>
        private async Task CreateResult(string code)
        {
            topEngineResultModel = new LTopEngineResultModel
            {
                EngineCode = code,
                StationId = Station?.StationID,
                Result = 0,
                brand = 0,
                CreateTime = DateTime.Now,
            };
            await RFIDDBHelper.LocalSQLHandler.LTopEngineResult.InsertAsync(topEngineResultModel);
            if (_screenConfig.Part.Available)
            {
                metasModel = new MaterialMetasModel
                {
                    EngineCode = code,
                    StationCode = Station?.StationID,
                    Result = 0
                };
            }
        }

        /// <summary>
        /// 更新结果数据
        /// </summary>
        /// <param name="code">发动机条码</param>
        /// <param name="tdResult">拧紧结果</param>
        private async void UpdateEngineResultToDB(string code, int tdResult)
        {
            try
            {
                if (topEngineResultModel != null && topEngineResultModel.Id > 0 && topEngineResultModel.EngineCode == code)
                {
                    topEngineResultModel.Result = tdResult;
                    topEngineResultModel.UpdateTime = DateTime.Now;
                    await RFIDDBHelper.LocalSQLHandler.LTopEngineResult.UpdateAsync(topEngineResultModel);
                }
                else
                {
                    topEngineResultModel = new LTopEngineResultModel
                    {
                        EngineCode = code,
                        StationId = Station?.StationID,
                        CreateTime = DateTime.Now,
                        Result = tdResult,
                        brand = 0,
                        UpdateTime = DateTime.Now
                    };
                    await RFIDDBHelper.LocalSQLHandler.LTopEngineResult.InsertAsync(topEngineResultModel);
                    if (metasModel != null)
                    {
                        if (metasModel.Id > 0)
                        {
                            RFIDDBHelper.MSSQLHandler.MaterialMetas.Update(metasModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("保存结果数据时异常", ex);
            }
        }

        /// <summary>
        /// 获取发动机详细信息
        /// </summary>
        /// <param name="code"></param>
        private EngineViewModel GetEngineCodeInfo(string code)
        {
            try
            {
                //获取发动机的机型
                return RFIDDBHelper.GetEngineOther(code, "");
            }
            catch (Exception ex)
            {
                Log.Error("处理发动机条码时出现异常", ex);
                return null;
            }
        }
        #endregion

        #region 零件绑定
        /// <summary>
        /// 零件码绑定
        /// </summary>
        /// <param name="pnoCode"></param>
        private void PartCodeBinding(string pnoCode)
        {
            try
            {
                if (metasModel?.Result == 1 || ViewModel.CodeBindStatus == "已绑定")
                {
                    ViewModel.PrintlnInfo($"已绑定零件号：{ViewModel.PnoCode}");
                    return;
                }
                ViewModel.PnoCode = pnoCode;
                if (EngineInfo == null || string.IsNullOrEmpty(EngineInfo.EngineCode))
                {
                    Log.Warning($"扫描零件码{pnoCode}时，发动机信息为空。");
                    ViewModel.CodeBindStatus = "无法绑定";
                    ViewModel.BindStatusColor = Color.Red;
                    return;
                }
                string engineCode = EngineInfo.EngineCode;
                string engineMTO = EngineInfo.EngineMTO;
                if (metasModel == null)
                {
                    metasModel = new MaterialMetasModel
                    {
                        EngineCode = engineCode,
                        StationCode = _screenConfig.Station.StationID,
                        MaterialCode = pnoCode
                    };
                }
                else
                {
                    metasModel.MaterialCode = pnoCode;
                    metasModel.ModifiedTime = DateTime.Now;
                }
                if (!string.IsNullOrEmpty(engineMTO))
                {
                    //EngineResultModel result = RFIDDBHelper.MSSQLHandler.EngineResult.Get(engineCode);
                    //if (result != null)
                    //    metasModel.ResultId = result.TID;
                    //还需要做派生校验
                    if (!CheckEnginePart(engineMTO, pnoCode))
                    {
                        Log.Warning("发动机{engineCode}与零件{pnoCode}不匹配", engineCode, pnoCode);
                        ViewModel.BindStatusColor = Color.Yellow;
                        ViewModel.CodeBindStatus = "零件绑定错误，请检查型号是否匹配！";
                        adam.AlarmWarning(true);
                        metasModel.Result = 0;
                        Task.Run(() => RFIDDBHelper.MSSQLHandler.SaveMaterialMetas(metasModel));

                        return;
                    }
                    //这里做绑定
                    metasModel.Result = 1;
                    Task.Run(() => RFIDDBHelper.MSSQLHandler.SaveMaterialMetas(metasModel));
                    ViewModel.CodeBindStatus = "已绑定";
                    ViewModel.BindStatusColor = Color.Green;
                }
                else
                {
                    ViewModel.CodeBindStatus = "没有发动机派生信息，无法进行绑定";
                    ViewModel.BindStatusColor = Color.Red;
                    Log.Warning($"扫描零件码{pnoCode}时，没有找到发动机派生信息.");
                    metasModel.Result = 0;
                    Task.Run(() => RFIDDBHelper.MSSQLHandler.SaveMaterialMetas(metasModel));
                }
            }
            catch (Exception ex)
            {
                ViewModel.BindStatusColor = Color.Red;
                ViewModel.CodeBindStatus = "无法绑定，系统异常";
                Log.Error("绑定零件码时异常--" + ex.StackTrace, ex);
            }
        }

        /// <summary>
        /// 零件派生校验
        /// </summary>
        /// <param name="engien"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public bool CheckEnginePart(string mto, string part)
        {
            try
            {
                part = part.Replace(" ", "");//零件码中间有空格
                List<DerivePnoModel> lstDerive = RFIDDBHelper.MSSQLHandler.DerivePno.GetAll(_screenConfig.Station.StationID);
                DerivePnoModel pnoModel = lstDerive.FirstOrDefault(d => mto.GetString(d.DeriveFeatureIndex.Split(',').Select(s => int.Parse(s) - 1).ToArray()).Equals(d.DeriveFeatureCode));
                if (pnoModel == null)
                    return true;//如果没有配置绑定规则，则不校验
                string pnoType = part.GetString(pnoModel.BarFeatureIndex.Split(',').Select(f => int.Parse(f) - 1).ToArray());
                if (pnoModel.PNO.Equals(pnoType))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("零件码匹配时出现错误", ex);
                return false;
            }

        }
        #endregion

        /// <summary>
        /// 发动机信息展示
        /// </summary>
        /// <param name="model"></param>
        private void DisPlayEngineInfo(EngineViewModel model)
        {
            if (model == null)
            {
                ViewModel.EngineCode = "";
                ViewModel.EngineMTO = "";
                ViewModel.EngineType = "";
            }
            else
            {
                ViewModel.EngineCode = model.EngineCode;
                ViewModel.EngineMTO = model.EngineMTO ?? "";
                ViewModel.EngineType = model.EngineName ?? "";
                if (lastMTO != "" && model.EngineMTO != lastMTO)
                {
                    //派生切换
                    ViewModel.EngineMTO += " 派生切换";
                }
                lastMTO = model.EngineMTO;
            }
        }

        #region OPC信号处理


        #endregion

        #region 拧紧数据处理

        /// <summary>
        /// 读取拧紧数据
        /// </summary>
        /// <param name="model"></param>
        public void OnReadTightenData(int toolId,TightenData model)
        {
            try
            {
                if (model != null)
                {
                    model.BoltNo = CurrentPointNum;
                    if (model.Result == 1)
                    {
                        CurrentPointNum++;
                    }
                    Debug.WriteLine($"{this.GetType().Name} -> 拧紧数据回调(Controller)->EngineCode   {model.EngineCode}");
                    Debug.WriteLine($"{this.GetType().Name} -> 拧紧数据回调(Controller)->BoltNo       {model.BoltNo}");
                    Debug.WriteLine($"{this.GetType().Name} -> 拧紧数据回调(Controller)->Torque       {model.Torque}");
                    Debug.WriteLine($"{this.GetType().Name} -> 拧紧数据回调(Controller)->Result       {model.Result}");
                    ViewModel.TighteningData = model;
                    ViewModel.CurTorque = model.Torque;
                    ViewModel.TorqueColor = model.Result == 1 ? Color.Green : Color.Red;
                    ViewModel.CurAngle = model.Angle.ToString() + "°";
                    Task.Run(()=>SaveTightenToLocal(model));
                }
                else
                    Debug.WriteLine($"{this.GetType().Name} -> 拧紧返回数据为NULL");
            }
            catch (Exception ex)
            {
                Log.Error("展示拧紧数据时异常，", ex);
            }
        }

        /// <summary>
        /// 保存拧紧数据到本地
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private void SaveTightenToLocal(TightenData data)
        {
            if (data == null)
                return;
            try
            {
                var model = TightenMapper.LocalMap(data);
                model.StationName = _screenConfig.Station.StationID;
                model.IsUpload = 0;
                RFIDDBHelper.LocalSQLHandler.TighteningService.Insert(model);
            }
            catch (Exception ex)
            {
                Log.Error("拧紧数据存储到本地数据库异常", ex);
            }
        }

        public bool GetTdResult()
        {
            if (lTightenConfigs == null)
                return false;
            return lTightenConfigs.All(c => tightenControllers[c.ToolId].GetResult());
        }

        /// <summary>
        /// 条码匹配结果
        /// </summary>
        /// <returns></returns>
        public bool GetPartResult()
        {
            if (!_screenConfig.Part.Available)
                return true;
            if (metasModel == null)
            {
                return false;
            }
            return metasModel.Result == 1;
        }
        #endregion

        #region Station

        /// <summary>
        /// 更新Station表信息
        /// </summary>
        /// <param name="station"></param>
        /// <param name="result"></param>
        private void UpdateStationInfo(StationModel station, int result)
        {
            try
            {
                if (station == null || station.TID < 1)
                {
                    return;
                }
                station.LastResult = result;
                station.LastTime = DateTime.Now;
                RFIDDBHelper.MSSQLHandler.StationService.Update(station);
            }
            catch (Exception ex)
            {
                Log.Error("UpdateStationInfo", ex);
            }
        }

        #endregion

        private void LogAlarm(string type, string context)
        {
            try
            {
                RFIDDBHelper.MSSQLHandler.SysMonitorAlarm.Insert(new SysMonitorAlarmModel
                {
                    StationID = _screenConfig.Station.StationID,
                    StationName = _screenConfig.Station.StationName,
                    AlarmType = type,
                    AlarmContent = context
                });
            }
            catch (Exception ex)
            {
                Log.Error("将报警信息写入服务端时异常", ex);
            }
        }

        #region 系统
        /// <summary>
        /// 防止启动时窗体闪烁
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        /// <summary>
        /// 当前工位需要拧紧的螺丝总数
        /// 所有枪的
        /// </summary>
        public int NeedNightenCount { get; private set; }

        /// <summary>
        /// 显示时钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }
     
        /// <summary>
        /// 关闭系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion 


        /// <summary>
        /// 解锁拧紧电枪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEnableTool_Click(object sender, EventArgs e)
        {
            foreach(int id in tightenControllers.Keys)
            {
                tightenControllers[id].EnableTool(true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSyncConfig_Click(object sender, EventArgs e)
        {
            btSyncConfig.Enabled = false;
            Task.Run(()=> SyncServerConfig());
        }

        private void SyncServerConfig()
        {
            try
            {
                bool ret = RFIDDBHelper.DownLoadServerConfig(_screenConfig.Station.StationID);
                ViewModel.PrintlnInfo($"同步{(ret ? "成功" : "失败")}！");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "同步参数失败！");
                ViewModel.PrintlnWarning("同步失败");
            }
            Invoke(new Action(()=>
            {
                btSyncConfig.Enabled = true;
            }));
        }
    }
}
