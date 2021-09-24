using System;
using System.Collections.Generic;
using System.Linq;

namespace RestroRouting.Domain.Entities
{
    public class Booth : BaseEntity
    {
        public Guid BoothId { get; }

        public int BoothNumber { get; set; }

        public string Name { get; }

        public string Status { get; private set; }

        // Assuming customer can order more than one order
        public List<Order> Orders { get; private set; }

        public Booth(int boothNumber, Guid boothId, string name)
        {
            this.BoothId = boothId;

            BoothNumber = boothNumber;

            this.Name = name;

            SetBoothStatus(BoothStatus.Unpaid);

            EffectiveStartDate = DateTime.Now;

            Orders = new List<Order>();
        }

        //Place Order
        public Guid PlaceOrder(List<MenuItemData> menuItems)
        {

     
            if (!menuItems.Any())
            {
                throw new Exception("Invalid request, atleast 1 menu item is required to place an order.");
            }

            var order = Order.CreateOrder(menuItems);

            Orders.Add(order);

            return order.OrderId;
        }


        private decimal TotalCost()
        {
            return Orders.Sum(o => o.OrderCost);
        }

        public void SetOrderStatus(Guid orderId, OrderStatus orderStatus)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);

            order?.SetOrderStatus(orderStatus);
        }

        public void SetBoothStatus(BoothStatus boothStatus)
        {
            Status = boothStatus.ToString();
        }
    }
}
