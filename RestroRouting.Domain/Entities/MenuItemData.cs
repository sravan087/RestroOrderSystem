using System;

namespace RestroRouting.Domain.Entities
{
    public class MenuItemData
    {
        public Guid MenuItemId { get; }

        public int Quantity { get; }

        public decimal ItemPrice { get; }

        public MenuItemType MenuItemType { get; set; }

        public MenuItemData(Guid menuItemId, int quantity, decimal itemPrice, MenuItemType menuItemType)
        {
            this.MenuItemId = menuItemId;
            this.ItemPrice = itemPrice;
            this.Quantity = quantity;
            this.MenuItemType = menuItemType;
        }
    }
}
