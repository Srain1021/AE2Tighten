using AE2Tightening.Configura;
using GodSharp.Opc;
using GodSharp.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodSharp.Opc.Da;
using AE2Tightening.Frame.ViewModel;

namespace AE2Tightening.Frame
{
    public class OpcController : IOpcController
    {
        private readonly Logging _logger;
        private readonly OpcDaClient opcClient;
        private Dictionary<string, OpcTagItem> dicItems = null;
        private DateTime heartTime = DateTime.Now;
        private readonly MainViewModels view;
        private System.Timers.Timer timer = new System.Timers.Timer();//心跳定时器
        private bool IsLocalShield = false;//是否屏蔽(双向的)
        private bool IsPLCShield = false;
        public bool IsShield { get => IsLocalShield && IsPLCShield; }
        public bool LineStatus { get; private set; }//线条状态
        public Action<bool> LineStatusChangedAction;
        public Action<string,string> DataChangedAction;
        public Action<bool> NetChangedAction;
        public OpcController(OpcConfig config, Logging logger = null)
        {
            _logger = logger;
            timer.Interval = 10 * 1000;
            timer.Elapsed += Timer_Elapsed;
            opcClient = new OpcDaClient(x =>
            {
                x.ProgId = config.Server.ProgId;
                x.Tags = config.Items.Where(xx => xx.Available).Select(xx => new OpcTagItem() { Id = xx.Id, ItemId = xx.ItemId, Misc = xx.Misc }).ToArray();
                x.DefaultGroupUpdateRate = 100;
            })
            {
                Shutdown = (s, client) =>
                {
                    NetChangedAction?.Invoke(false);
                    _logger.Warn("OPC Shutdown," + s);
                },
                DataChange = OpcDataChanged
            };
        }

        /// <summary>
        /// opc数据响应
        /// </summary>
        /// <param name="para"></param>
        /// <param name="items"></param>
        private void OpcDataChanged(DataChangeParameter para,IEnumerable<OpcTagItem> items)
        {
            if (items != null && items.Count() > 0)
                Task.Run(() => ProcessOpcData(items));
        }
        /// <summary>
        /// opc消息处理
        /// </summary>
        /// <param name="items"></param>
        private void ProcessOpcData(IEnumerable<OpcTagItem> items)
        {
            foreach (var item in items)
            {
                if (item == null)
                    continue;
                try
                {
                    if (item.Value == null)
                        continue;
                    switch (item.Misc)
                    {
                        case "Heartbeat":
                            if (item.Value.ToString().ToLower() == "false" || item.Value.ToString() == "0")//这里需要确认值类型
                            {
                                heartTime = DateTime.Now;
                            }
                            break;
                        case "ReadShieldSystem":
                            IsPLCShield = Convert.ToBoolean(item.Value);
                            DataChangedAction?.Invoke("ReadShieldSystem", IsShield.ToString());
                            break;
                        case "NoPass"://这里可以做信号校验
                            LineStatus = Convert.ToBoolean(item.Value);
                            DataChangedAction?.Invoke("NoPass", item.Value.ToString());
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"读取opc节点[{item.Misc}]信息异常",ex);
                }
                
            }
        }

        public void Close()
        {
            if(opcClient != null && opcClient.Connected)
            {
                opcClient.Disconnect();
            }
        }
        /// <summary>
        /// 启动
        /// </summary>
        public async void Open()
        {
            try
            {
                if (opcClient?.Connected == false)
                {
                    await Task.Run(() =>
                    {
                        try
                        {
                            opcClient.Connent();
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("OPC通讯连接异常", ex);
                        }
                    });
                    NetChangedAction?.Invoke(opcClient.Connected);
                    dicItems = opcClient.OpcItems.ToDictionary(x => x.Misc?.ToString(), x => x);
                    if (dicItems.ContainsKey("Heartbeat"))
                    {
                        timer.Start();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error("opc启动异常", e);
            }
        }

        /// <summary>
        /// 放行
        /// </summary>
        public async Task<bool> RunLine()
        {
            //return;//还没有调试,暂时屏蔽
            if (WriteBeforeCheck("NoPass"))
            {
                bool state = opcClient.AsycnWriter(dicItems["NoPass"].ItemId, 0);
                _logger.Info($"写入PLC放行{(state?"成功":"失败")}");
                await Task.Delay(500);
                return LineStatus;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 停线
        /// </summary>
        public async Task<bool> StopLine()
        {
            //return;//还没有调试,暂时屏蔽
            if (WriteBeforeCheck("NoPass"))
            {
                bool state = opcClient.AsycnWriter(dicItems["NoPass"].ItemId, 1);
                _logger.Info($"写入PLC禁止放行{(state ? "成功" : "失败")}");
                await Task.Delay(500);
                if(LineStatus)
                {
                    _logger.Warn("发送停线信号，但是没有停线。");
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 心跳定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(opcClient?.Connected == false)
            {
                timer.Stop();
                return;
            }
            if((DateTime.Now - heartTime).TotalSeconds > 15)
            {
                _logger.Warn("PLC心跳超时，连接断开");
                NetChangedAction?.Invoke(false);
                timer.Stop();
                return;
            }
            Heartbeat();
        }

        /// <summary>
        /// 心跳
        /// </summary>
        private void Heartbeat()
        {
            if(WriteBeforeCheck("Heartbeat"))
            {
                opcClient.AsycnWriter(dicItems["Heartbeat"].ItemId, 1);
            }
        }

        /// <summary>
        /// 屏蔽
        /// </summary>
        /// <param name="state"></param>
        public void ShieldPLC(bool state)
        {
            if (WriteBeforeCheck("ReadShieldSystem"))
            {
                if (opcClient.AsycnWriter(dicItems["ReadShieldSystem"].ItemId, state ? 1 : 0))
                {
                    IsLocalShield = state;
                }
            }
            DataChangedAction?.Invoke("ReadShieldSystem", IsShield.ToString());
        }

        /// <summary>
        /// 写值前的状态检查
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool WriteBeforeCheck(string item)
        {
            if (opcClient == null)
            {
                _logger.Warn($"写OPC[{item}]节点时，opcClient为null。");
                return false;
            }
            else if (opcClient.Connected == false)
            {
                return false;
            }
            else if (!dicItems.ContainsKey(item))
            {
                _logger.Warn($"没有找到[{item}]节点，无法写值。");
                return false;
            }
            if(item != "ReadShieldSystem" && IsShield)
            {
                return false;
            }
            else
                return true;
        }
    }
}
