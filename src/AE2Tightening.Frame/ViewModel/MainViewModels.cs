using AE2Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AE2Tightening.Frame.ViewModel
{
    public class MainViewModels : ViewModelBase
    {
        private string engineCode = "";//发动机码
        private string enginemto = "";//发动机派生
        private string enginetype = "";//发动机机型
        private string pnoCode = "";//零件码
        private string pnoBindStatus = "";//零件绑定状态
        private string warnText = "";//报警信息
        private string tdResult = "";//拧紧结果
        private string logText = "";//日志记录
        private bool tdNet = false;//拧紧网络
        private bool tdNet2 = false;//拧紧网络
        private bool rfidNet = false;//RFID网络
        private bool opcNet = false;//opc网络
        private bool adamNet = false;//adam网络
        private bool scanNet = false;//扫描枪网络
        private bool shield = false;//线体控制
        private bool stopLine = false;
        private string automodel = "手动";
        private Color bindLabelColor = Color.Black;
        private double torque;
        private string angle;
        private Color resultColor;
        private TightenData tightendata;
        public MainViewModels()
        {
        }

        #region picturebox控件绑定模型
        //#region RFID网络
        ////对外暴露参数
        //public bool RFIDNet
        //{
        //    get => rfNet;
        //    set
        //    {
        //        rfNet = value;
        //        RFIDNetImage = rfNet ? ok : ng;
        //    }
        //}
        //public Image RFIDNetImage//实际绑定参数
        //{
        //    get => rfNetImage;
        //    set
        //    {
        //        SetProperty(ref rfNetImage, value);
        //    }
        //}
        //#endregion
        //#region 拧紧机网络
        ////对外暴露参数
        //public bool TightenNet
        //{
        //    get => tdNet;
        //    set
        //    {
        //        tdNet = value;
        //        TightenNetImage = tdNet ? ok : ng;
        //    }
        //}
        //public Image TightenNetImage//实际绑定参数
        //{
        //    get => tdNetImage;
        //    set
        //    {
        //        SetProperty(ref tdNetImage, value);
        //    }
        //}
        //#endregion
        //#region OPC网络
        ////对外暴露参数
        //public bool OPCNet
        //{
        //    get => opcNet;
        //    set
        //    {
        //        opcNet = value;
        //        OPCNetImage = opcNet ? ok : ng;
        //    }
        //}
        //public Image OPCNetImage//实际绑定参数
        //{
        //    get => opcNetImage;
        //    set
        //    {
        //        SetProperty(ref opcNetImage, value);
        //    }
        //}
        //#endregion
        //#region ADAM网络
        ////对外暴露参数
        //public bool AdamNet
        //{
        //    get => adamNet;
        //    set
        //    {
        //        adamNet = value;
        //        AdamNetImage = adamNet ? ok : ng;
        //    }
        //}
        //public Image AdamNetImage//实际绑定参数
        //{
        //    get => adamNetImage;
        //    set
        //    {
        //        SetProperty(ref adamNetImage, value);
        //    }
        //}
        //#endregion
        #endregion

        #region 状态监控控件
        public bool RFIDNet
        {
            get => rfidNet;
            set => SetProperty(ref rfidNet, value);
        }
        public bool ADAMNet
        {
            get => adamNet;
            set => SetProperty(ref adamNet, value);
        }
        public bool PLCNet
        {
            get => opcNet;
            set => SetProperty(ref opcNet, value);
        }
        public bool Tighten1Net
        {
            get => tdNet;
            set => SetProperty(ref tdNet, value);
        }
        public bool TightenNet2
        {
            get => tdNet2;
            set => SetProperty(ref tdNet2, value);
        }
        public bool ShieldStatus
        {
            get => shield;
            set => SetProperty(ref shield, value);
        }
        public bool ScantoolNet
        {
            get => scanNet;
            set => SetProperty(ref scanNet, value);
        }
        /// <summary>
        /// 点位停线状态
        /// </summary>
        public bool Pass {
            get => stopLine;
            set => SetProperty(ref stopLine, value);
        }
       
        #endregion

        #region 文本控件绑定模型
        /// <summary>
        /// 发动机码
        /// </summary>
        public string EngineCode
        {
            get => engineCode;
            set => SetProperty(ref engineCode, value);
        }
        /// <summary>
        /// 派生
        /// </summary>
        public string EngineMTO
        {
            get => enginemto;
            set => SetProperty(ref enginemto, value);
        }
        /// <summary>
        /// 发动机类型
        /// </summary>
        public string EngineType
        {
            get => enginetype;
            set => SetProperty(ref enginetype, value);
        }
        /// <summary>
        /// 零件码
        /// </summary>
        public string PnoCode
        {
            get => pnoCode;
            set => SetProperty(ref pnoCode, value);
        }
        /// <summary>
        /// 零件绑定状态
        /// </summary>
        public string CodeBindStatus
        {
            get => pnoBindStatus;
            set => SetProperty(ref pnoBindStatus, value);
        }
        /// <summary>
        /// 绑定控件字体颜色
        /// </summary>
        public Color BindStatusColor
        {
            get => bindLabelColor;
            set => SetProperty(ref bindLabelColor, value);
        }
        /// <summary>
        /// 拧紧总结果
        /// </summary>
        public string TightenResult
        {
            get => tdResult;
            set => SetProperty(ref tdResult, value);
        }
        /// <summary>
        /// 拧紧扭矩
        /// </summary>
        public double CurTorque
        {
            get => torque;
            set => SetProperty(ref torque, value);
        }
        public string CurAngle
        {
            get => angle;
            set => SetProperty(ref angle, value);
        }
        /// <summary>
        /// 界面底部报警提示
        /// </summary>
        public string AppWarnText
        {
            get => warnText;
            set => SetProperty(ref warnText, value);
        }

        public string LogText
        {
            get => logText;
            set => SetPropertyEqual(ref logText, value);
        }

        public string AutoMode
        {
            get => automodel;
            set => SetProperty(ref automodel, value);
        }

        public Color TorqueColor
        {
            get => resultColor;
            set
            {
                SetProperty(ref resultColor, value);
            }
        }
        #endregion

        public Action<string> PrintlnInfo;

        public Action<string> PrintlnWarning;

        public Action OnWorkCompate;
       

        #region 数据控件绑定
        public TightenData TighteningData
        {
            get => tightendata;
            set => SetPropertyEqual(ref tightendata, value);

        }
        #endregion
    }
}
