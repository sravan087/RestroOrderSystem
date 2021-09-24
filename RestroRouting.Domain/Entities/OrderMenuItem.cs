using System;

namespace RestroRouting.Domain.Entities
{
    public class OrderMenuItem
    {
        public Guid OrderMenuItemId { get;  }

        public Guid MenuItemId { get; }
        public int Quantity { get; private set; }
        public decimal Cost { get; private set; }
        public string ItemType { get; }
        public bool ReadyToServe { get; private set; }

        private OrderMenuItem()
        {

        }

        private OrderMenuItem(Guid menuItemId, int quantity, decimal itemPrice, MenuItemType menuItemType) 
        {
            this.MenuItemId = menuItemId;
            this.OrderMenuItemId = Guid.NewGuid();
            this.Quantity = quantity;
            this.ItemType = menuItemType.ToString();

            CalculateCost(itemPrice);
        }

        internal static OrderMenuItem CreateForMenuItem(Guid menuItemId, int quantity, decimal itemPrice, MenuItemType menuItemType)
        {
            return new OrderMenuItem(menuItemId, quantity, itemPrice, menuItemType);
        }

   
        internal void CalculateCost(decimal itemPrice)
        {
            this.Cost = this.Quantity * itemPrice;
        }

        public void SetReadyToServe()
        {
            ReadyToServe = true;
        }
    }
}
