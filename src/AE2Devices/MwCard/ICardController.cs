using System;

namespace AE2Devices
{
    public interface ICardController : IDevice
    {
        event Action<CardInfo> SwipedEvent;
    }
}
