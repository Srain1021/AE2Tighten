using System;
using System.Text;
using System.Threading.Tasks;
using AE2Tightening.Configura;
using AE2Tightening.Frame.Data;
using AE2Tightening.Frame.ViewModel;
using AE2Tightening.Models;
using GodSharp.AtlasCopco.OpenProtocol;
using GodSharp.Logging.Abstractions;

namespace AE2Tightening.Frame
{
    public class GSTightenController : ITightenController
    {
        private TcpToolClient client;//拧紧机通讯接口
        private readonly Logging _logger;//日志接口
        public TightenDataCaChe TightenDatas { get; }//拧紧数据缓存
        private readonly ScreenConfig tdConfig;//配置信息
        private readonly MainViewModels view;
        private string currentEngineCode = "";
        public GSTightenController(ScreenConfig config, Logging logger,MainViewModels viewModel)
        {
            _logger = logger;
            tdConfig = config;
            view = viewModel;
            TightenDatas = new TightenDataCaChe(tdConfig.Tighten.BoltCount);
        }

        public event OnVinNumberChangedDelegate OnVinNumberChanged;

        public void Close()
        {
            client?.Disconnect();
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
                if (client == null)
                {
                    client = new TcpToolClient(tdConfig.Tighten.Socket.Host, tdConfig.Tighten.Socket.Port)
                    {
                        OnConnected = OnConnected,
                        OnMessage = OnTightenMessage,
                        OnDisconnected = () => {
                            view.TightenNet = client.Connected;
                            view.LogText = "与拧紧机断开通讯";
                            }
                    };
                }
                return await Task.Run(() => Connect());
            }
            catch (Exception ex)
            {
                _logger.Error("拧紧通讯初始化失败", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 建立通讯
        /// </summary>
        /// <returns></returns>
        private bool Connect()
        {
            try
            {
                if (client.Connect())
                {
                    if (client.CommunicationStart())
                    {
                        
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error("拧紧通讯初始化失败", ex);
                view.LogText = "拧紧机通讯连接失败，" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 网络连接
        /// </summary>
        private void OnConnected()
        {
            try
            {
                _logger.Info($"拧紧机连接成功，{tdConfig.Tighten.Socket.Host}:{tdConfig.Tighten.Socket.Port}");
                view.TightenNet = true;
                client.LastTighteningResultDataSubscribe();
                client.VehicleIdNumberSubscribe();
                view.LogText = $"拧紧机{tdConfig.Tighten.Socket.Host}已连接。";

            }
            catch (Exception ex)
            {
                _logger.Error($"拧紧机OnConnected", ex);
            };
        }
        /// <summary>
        /// 拧紧机消息接收
        /// </summary>
        /// <param name="msg"></param>
        private void OnTightenMessage(OpMessage msg)
        {
            try
            {
                _logger.Info($"接收到拧紧机信息{msg.SequenceNumber}");
                if (msg.SequenceNumber == 0061)//SequenceNumber就是Mid
                {
                    client.LastTighteningResultDataAcknowledge();
                    ReadLastTightenData(msg);
                }
                else if (msg.SequenceNumber == 0052)
                {
                    client.VehicleIdNumberAcknowledge();
                    string vinnumber = msg.Get<string>("VinNumber").Trim();
                    ReadVinNumber(vinnumber);
                }
                else if (msg.SequenceNumber == 0005)
                {
                    _logger.Debug("接收到拧紧机0005消息，消息体含命令：" + msg.Get<string>("MidAccepted"));
                }
            }
            catch (Exception ex)
            {
                _logger.Error("OnTightenMessage", ex);
            }
        }
        /// <summary>
        /// 读取拧紧数据
        /// </summary>
        /// <param name="msg"></param>
        private void ReadLastTightenData(OpMessage msg)
        {
            try
            {
                TighteningResultModel result = new TighteningResultModel();
                result.EngineCode = msg.Get<string>("VinNumber").Trim();
                result.Torque = !decimal.TryParse(msg.Get<string>("Torque"), out decimal torque) ? -1 : torque / 100.0m;
                result.Angle = !decimal.TryParse(msg.Get<string>("Angle"), out decimal angle) ? -1 : angle;
                result.Result = !int.TryParse(msg.Get<string>("TighteningStatus"), out int status3) ? -1 : status3;
                result.ResultTime = result.CreateTime = DateTime.Now;
                DisplayData(result);
                SaveTightenData(result);
            }
            catch (Exception ex)
            {
                _logger.Error("ReadLastTightenData", ex);
            }
        }

        private void ReadVinNumber(string code)
        {
            if(code.Length > 17)
            {
                code = code.Substring(0, 17);
            }
            OnVinNumberChanged?.Invoke(code);
        }

        /// <summary>
        /// 数据展示
        /// </summary>
        /// <param name="model"></param>
        private void DisplayData(TighteningResultModel model)
        {
            //拧紧数据控件绑定还没有完成
            TightenDatas.AddTightenData(model);
            view.TighteningData = model;
            if (TightenDatas.IsTightenOK())
            {
               view.TightenResult = "OK";
            }
        }

        /// <summary>
        /// 保存拧紧数据
        /// </summary>
        /// <param name="model"></param>
        private void SaveTightenData(TighteningResultModel model)
        {
            try
            {
                model.StationID = tdConfig.Station.StationID;
                model.StationName = tdConfig.Station.StationName;
                model.ResultTime = DateTime.Now;
                model.CreateTime = model.ResultTime;
                model.EngineCode = currentEngineCode;
                //暂时屏蔽
                RFIDDBHelper.MSSQLHandler.TightenService.Insert(model);
            }
            catch (Exception ex)
            {
                view.LogText = "拧紧数据存储到服务器上失败";
                _logger.Error("拧紧数据存储到服务器上失败", ex);
            }
            try
            {
                int i = RFIDDBHelper.LocalSQLHandler.TighteningService.Insert(new Lite.Model.TightenModel()
                {
                    StationName = tdConfig.Station.StationName,
                    EngineCode = currentEngineCode,
                    BoltNo = model.BoltNO,
                    Torque = model.Torque,
                    Angle = model.Angle,
                    Result = model.Result
                });
                if(i < 1)
                {
                    view.LogText = "拧紧数据存储到本地数据库失败";
                }
            }
            catch (Exception ex)
            {
                _logger.Error("拧紧数据存储到本地数据库异常", ex);
                view.LogText = "拧紧数据存储到本地数据库异常";
            }
            
           

        }
        /// <summary>
        /// 手动设置当前条码
        /// </summary>
        /// <param name="code"></param>
        public void SendEngineCode(string code)
        {
            _logger.Info($"发送发动机条码{code}到拧紧机");
            currentEngineCode = code;
            TightenDatas.ClearTightenData();
            if (tdConfig.Tighten.CodeRequest)
            {
                client?.VehicleIdNumberDownloadRequest(code);
            }
        }

        public void Test(TighteningResultModel model)
        {
            DisplayData(model);
        }
    }
}
