using System;
using System.Collections.Generic;
using System.Linq;

namespace RestroRouting.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; }

        // Assuming an order has more than one menu item
        public List<OrderMenuItem> OrderMenuItems { get; }

        public decimal OrderCost { get; private set; }

        public string Status { get; private set; }

        private Order()
        {
            OrderId = Guid.NewGuid();
            OrderMenuItems = new List<OrderMenuItem>();
        }

        private Order(List<MenuItemData> orderItems) : this()
        {
            orderItems.ForEach(o => 
            {
                var menuItem = OrderMenuItem.CreateForMenuItem(o.MenuItemId, o.Quantity, o.ItemPrice, o.MenuItemType);
                OrderMenuItems.Add(menuItem);
            
            });

            CalculateOrderValue();

            SetOrderStatus(OrderStatus.Placed);
        }

        internal static Order CreateOrder(List<MenuItemData> menuItems)
        {
            return new Order(menuItems);
        }
        private void CalculateOrderValue()
        {
            OrderCost = OrderMenuItems.Sum(o => o.Cost);
        }

        public void SetOrderStatus(OrderStatus orderStatus)
        {
            Status = orderStatus.ToString();
        }


    }
}
