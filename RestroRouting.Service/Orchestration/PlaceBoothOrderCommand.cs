using MediatR;
using RestroRouting.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RestroRouting.Service.Orchestration
{
    public class PlaceBoothOrderCommand : IRequest<Guid>
    {
        public int BoothNumber { get; }

        public Guid BoothId { get; }
        public string CustomerName { get; }
        public List<MenuItemData> MenuItems { get; }
       
        public PlaceBoothOrderCommand(int boothNumber, Guid boothId, string customerName, List<MenuItemData> menuItems)
        {
            this.BoothNumber = boothNumber;
            this.BoothId = boothId;
            this.CustomerName = customerName;
            this.MenuItems = menuItems;
        }
    }

}
