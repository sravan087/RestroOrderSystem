using RestroRouting.Domain;
using RestroRouting.Domain.Entities;
using RestroRouting.Service.Orchestration;
using System;
using System.Threading.Tasks;

namespace RestroRouting.Service.Logic.Interfaces
{
    public  interface IBoothService
    {
        Task<Booth> UpdateOrderStatus(Guid boothId, Guid orderId, OrderStatus orderStatus);

        Task<Booth> PlaceOrder(PlaceBoothOrderCommand placeBoothOrderCommand);

        Task<Booth> UpdateOrderMenuItemStatus(Guid boothId, Guid orderId, Guid menuItemId);

        Task<Booth> UpdateOrderStatusToComplete(Guid boothId, Guid orderId);
    }
}
