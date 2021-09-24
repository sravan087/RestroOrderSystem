using MediatR;
using System;

namespace RestroRouting.Service.Orchestration
{
    public class MenuItemCompletedNotification : INotification
    {
        public Guid BoothId { get; }
        public Guid OrderId { get; }
        public Guid MenuItemId { get; }

        public MenuItemCompletedNotification(Guid boothId, Guid orderId, Guid menuItemId)
        {
            BoothId = boothId;
            OrderId = orderId;
            MenuItemId = menuItemId;
        }
    }
}
