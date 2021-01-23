using System;

namespace AE2Devices
{
    public class CardInfo
    {
        public string CardId { get;}

        public DateTime SwipeTime { get;}

        public CardInfo(string id)
        {
            CardId = id;
            SwipeTime = DateTime.Now;
        }
    }
}
