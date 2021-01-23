using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MwCard;
using AE2Tightening.Configura;

namespace AE2Devices
{
    internal class MwCardController : ICardController
    {
        public bool NetStatus{ get;private set; }
        public Action<IDevice, bool> NetChangedAction { get; set; }

        public event Action<CardInfo> SwipedEvent;
        private MwCardClient client = null;

        public MwCardController(MwCardConfig config)
        {
            client = new MwCardClient(--config.Port, config.BaudRate);
        }

        public void Close()
        {
            if(client != null)
            {
                client.Close();
            }
        }

        public bool Open()
        {
            try
            {
                if (client.Open())
                {
                    Thread swipeThread = new Thread(new ThreadStart(ReadCard));
                    swipeThread.IsBackground = true;
                    swipeThread.Start();
                    NetChangedAction?.Invoke(this, true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            NetChangedAction?.Invoke(this, false);
            return false;
        }

        public void OpenAsync()
        {
            Task.Run(() => Open());
        }

        /// <summary>
        /// 等待刷卡
        /// </summary>
        private void ReadCard()
        {
            while (true)
            {
                try
                {
                    if (client == null || client.Opened == false)
                        return;
                    //寻卡
                    if (client.Seek())
                    {
                        client.Beep(500);
                        //卡号
                        string msg = client.CardSerialNumber.ToString("x2");
                        if (msg.Length > 0)
                        {
                            SwipedEvent?.Invoke(new CardInfo(msg));
                        }
                    }
                    Thread.Sleep(500);
                }
                catch
                {
                    Thread.Sleep(2000);
                }
            }
        }

    }
}
