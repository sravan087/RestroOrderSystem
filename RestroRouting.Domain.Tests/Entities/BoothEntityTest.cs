using FluentAssertions;
using NUnit.Framework;
using RestroRouting.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RestroRouting.Domain.Tests.Entities
{

    [TestFixture]
   public  class BoothEntityTest
    {
  

        [Test]
        public void Given_Booth_Number_And_Customer_Name_Reserves_The_Booth()
        {
            // Arrange

            // Act
            var booth = new Booth(1, "sam");

            // Assert
            booth.BoothId.Should().NotBeEmpty();
            booth.Status.Should().Be(BoothStatus.Unpaid.ToString());
            booth.Orders.Should().NotBeNull();
        }


        [Test]
        public void Given_Menu_Items_Creates_Kitchen_Order()
        {
            // Arrange
            // Act
            var menuItems = new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 1, 1.5M, MenuItemType.Desert) };           
            var booth = new Booth(1, "sam");
            var result = booth.PlaceOrder(menuItems);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Test]
        public void Given_Empty_Menu_Items_Raises_Exception_On_Kitchen_Order()
        {
            // Arrange
            // Act
            var menuItems = new List<MenuItemData> {  };          
            var booth = new Booth(1, "sam");


            // Assert
            Assert.Throws<Exception>(() => booth.PlaceOrder(menuItems));
        }

        [Test]
        public void Given_Order_And_Status_Updates_OrderStatus()
        {
            // Arrange 
            // Act
            var booth = new Booth(1, "sam");
            var menuItems = new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 1, 1.5M, MenuItemType.Desert) };
            var orderId = booth.PlaceOrder(menuItems);

            booth.SetOrderStatus(orderId, OrderStatus.InProgress);



            // Assert
            booth.BoothId.Should().NotBeEmpty();
            booth.Status.Should().Be(BoothStatus.Unpaid.ToString());
            booth.Orders.Should().NotBeNull();
        }

        [Test]
        public void Given_Order_Calculates_Cost()
        {
            // Arrange 
            // Act
            var booth = new Booth(1, "sam");
            var menuItems = new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 2, 1.5M, MenuItemType.Desert) };
            var orderId = booth.PlaceOrder(menuItems);


            // Assert            
            booth.Orders[0].OrderCost.Should().Be(3); 
        }

        [Test]
        public void Given_New_Order_Sets_Status_To_Placed()
        {
            // Arrange 
            // Act
            var booth = new Booth(1, "sam");
            var menuItems = new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 2, 1.5M, MenuItemType.Desert) };
            var orderId = booth.PlaceOrder(menuItems);


            // Assert            
            booth.Orders[0].Status.Should().Be(OrderStatus.Placed.ToString());
        }

        [Test]
        public void Given_New_Order_Calculates_Menu_Item_Cost()
        {
            // Arrange 
            // Act
            var booth = new Booth(1, "sam");
            var menuItems = new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 2, 1.5M, MenuItemType.Desert) };
            booth.PlaceOrder(menuItems);


            // Assert            
            booth.Orders[0].OrderMenuItems[0].Cost.Should().Be(3);
        }
    }
}
