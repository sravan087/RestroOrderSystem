using MediatR;
using RestroRouting.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RestroRouting.Service.Orchestration
{
    public class OrderPlacedNotification : IRequest
    {
        public Guid BoothId { get; }
        public Guid OrderId { get; }
        public List<MenuItemData> MenuItems { get; }

        public OrderPlacedNotification(Guid boothId, Guid orderId, List<MenuItemData> menuItems)
        {
            this.BoothId = boothId;
            this.OrderId = orderId;
            this.MenuItems = menuItems;
        }
    }
}
