using RestroRouting.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Logic
{
    public interface IMenuItemProcesser
    {
        Task<bool> ProcessMenuItem(Guid boothId, Guid orderId, MenuItemData menuItem, CancellationToken cancellationToken);
    }
}
