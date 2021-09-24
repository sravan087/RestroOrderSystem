using RestroRouting.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Logic.Interfaces
{
    public  interface IMenuItemFactoryProcesser
    {
        Task ProcessMenuItems(Guid boothId, Guid orderId, List<MenuItemData> menuItems, CancellationToken cancellationToken);
    }
}
