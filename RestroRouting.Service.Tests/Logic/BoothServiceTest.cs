using FluentAssertions;
using Moq;
using NUnit.Framework;
using RestroRouting.Data.BoothRepository.Interfaces;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Domain;
using RestroRouting.Domain.Entities;
using RestroRouting.Service.Logic;
using RestroRouting.Service.Logic.Interfaces;
using RestroRouting.Service.Orchestration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Tests.Logic
{
    [TestFixture]
    public class BoothServiceTest
    {
        private readonly Mock<IBoothData> _mockBoothData;
        private readonly Mock<IUnitofWork> _mockUow;

        public BoothServiceTest()
        {
            _mockBoothData = new Mock<IBoothData>();
            _mockUow = new Mock<IUnitofWork>();
        }

        [Test]
        public async Task Given_Order_To_Place_Saves_The_Order()
        {
            // Arrange
            var placeBoothOrderCommand = new PlaceBoothOrderCommand(1, "sam", new List<MenuItemData>
            {
                new MenuItemData(Guid.NewGuid(),1, 3.99M, Domain.MenuItemType.Fries)
            });

            _mockBoothData.Setup(x => x.Save(It.IsAny<Booth>()));

            _mockUow.Setup(x => x.CommitAsync(CancellationToken.None)).ReturnsAsync(true);

            // Act
            IBoothService boothService = new BoothService(_mockBoothData.Object, _mockUow.Object);
            var result = await boothService.PlaceOrder(placeBoothOrderCommand);

            // Assert
            result.Should().NotBeNull();
            result.BoothId.Should().NotBeEmpty();
            result.Orders.Should().HaveCount(1);
            _mockBoothData.Verify(x => x.Save(It.IsAny<Booth>()), Times.Once);


        }

        [Test]
        public async Task Given_Order_To_Update_Sets_The_Order_Status()
        {
            // Arrange
            var boothId = new Guid("8454e9f0-3206-4900-8318-2e0b26ccab1f");
            var orderId = new Guid("e0c2ce94-e891-4c88-a08f-c0f20e4e1bc2");

            var booth = new Booth(1, "sam");
            var menuItems = new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 1, 1.5M, MenuItemType.Desert) };
            booth.PlaceOrder(menuItems);





            _mockBoothData.Setup(x => x.Get(boothId)).ReturnsAsync(booth);

            _mockUow.Setup(x => x.CommitAsync(CancellationToken.None)).ReturnsAsync(true);

            // Act
            IBoothService boothService = new BoothService(_mockBoothData.Object, _mockUow.Object);
            var result = await boothService.UpdateOrderStatus(boothId, orderId, Domain.OrderStatus.Complete);

            // Assert
            result.Should().NotBeNull();            


        }

        [Test]
        public async Task Given_Order_To_Update_Sets_The_Menu_Item_Status()
        {
            // Arrange
            var boothId = new Guid("8454e9f0-3206-4900-8318-2e0b26ccab1f");
            var booth = new Booth(1, "sam");
            var menuItems = new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 1, 1.5M, MenuItemType.Desert) };
            booth.PlaceOrder(menuItems);

            _mockBoothData.Setup(x => x.Get(boothId)).ReturnsAsync(booth);

            // Act
            IBoothService boothService = new BoothService(_mockBoothData.Object, _mockUow.Object);
            var result = await boothService.UpdateOrderMenuItemStatus(boothId, booth.Orders[0].OrderId, booth.Orders[0].OrderMenuItems[0].OrderMenuItemId);

            // Assert
            result.Orders[0].OrderMenuItems[0].ReadyToServe.Should().BeTrue();
            _mockBoothData.Verify(x => x.Update(It.IsAny<Booth>()), Times.Once);

        }
    }
}
