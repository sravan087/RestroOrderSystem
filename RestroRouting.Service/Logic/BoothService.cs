using RestroRouting.Data.BoothRepository.Interfaces;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Domain;
using RestroRouting.Domain.Entities;
using RestroRouting.Service.Logic.Interfaces;
using RestroRouting.Service.Orchestration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestroRouting.Service.Logic
{
    public class BoothService : IBoothService
    {
        private readonly IBoothData _boothData;
        private readonly IUnitofWork _uow;

        public BoothService(IBoothData boothData, IUnitofWork unitofWork)
        {
            _boothData = boothData;
            _uow = unitofWork;
        }

        public async Task<Booth> PlaceOrder(PlaceBoothOrderCommand placeBoothOrderCommand)
        {
            var booth = new Booth(placeBoothOrderCommand.BoothNumber, placeBoothOrderCommand.BoothId, placeBoothOrderCommand.CustomerName);
            booth.PlaceOrder(placeBoothOrderCommand.MenuItems);
            await _boothData.Save(booth);           
            return booth;
        }

        public async Task<Booth> UpdateOrderStatus(Guid boothId, Guid orderId, OrderStatus orderStatus)
        {
            var booth = await _boothData.Get(boothId);

            if (booth == null)
            {
                throw new Exception("Invalid request");
            }

            booth.SetOrderStatus(orderId, orderStatus);

            return booth;
        }


        public async Task<Booth> UpdateOrderMenuItemStatus(Guid boothId, Guid orderId, Guid menuItemId)
        {
            var booth = await _boothData.Get(boothId);
            if (booth == null)
            {
                throw new Exception("Invalid request");
            }
            var order = booth.Orders.First(o => o.OrderId == orderId);

            var menuItem = order.OrderMenuItems.First(o => o.MenuItemId == menuItemId);

             menuItem.SetReadyToServe();

            _boothData.Update(booth);

            return booth;
        }

        public async Task<Booth> UpdateOrderStatusToComplete(Guid boothId, Guid orderId)
        {
            var booth = await _boothData.Get(boothId);
            if (booth == null)
            {
                throw new Exception("Invalid request");
            }

            bool isCompleted = true;
            booth.Orders.ForEach(o => 
            {
                o.OrderMenuItems.ForEach(m => 
                {
                    if(!m.ReadyToServe)
                    {
                        isCompleted = false;
                        return;
                    }
                });
            });

            if(isCompleted)
            {
               return await UpdateOrderStatus(boothId, orderId, OrderStatus.Complete);
            }

            return booth;
        }
    }
}
