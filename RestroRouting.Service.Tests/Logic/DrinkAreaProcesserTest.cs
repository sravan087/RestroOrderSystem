using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Domain.Entities;
using RestroRouting.Service.Logic;
using RestroRouting.Service.Logic.Interfaces;
using RestroRouting.Service.Orchestration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Tests.Logic
{
    [TestFixture]
   public class DrinkAreaProcesserTest
    {
        private readonly Mock<IBoothService> _mockBoothService;
        private readonly Mock<IUnitofWork> _mockUow;
        private readonly Mock<IMediator> _mockMediator;


        public DrinkAreaProcesserTest()
        {
            _mockBoothService = new Mock<IBoothService>();
            _mockUow = new Mock<IUnitofWork>();
            _mockMediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Given_Drink_Item_Returns_Completed_Task()
        {
            // Arrange
            var boothId = new Guid("44304ebe-1bdb-4887-a31b-c1dab224a6f2");
            var orderId = new Guid("4cdd3778-d297-499a-b07b-3c14afcdfc0c");
            var menuItem = new MenuItemData(new Guid("b99ca376-2b74-4276-908c-d27243d63ee7"), 1, 2.5M, Domain.MenuItemType.Drink);

            // Act
            IMenuItemProcesser menuItemProcesser = new DrinkAreaProcesser(_mockBoothService.Object, _mockUow.Object, _mockMediator.Object);
            var result =  await menuItemProcesser.ProcessMenuItem(boothId, orderId, menuItem, CancellationToken.None);

            // Assert
            result.Should().Be(true);
            _mockBoothService.Verify(x => x.UpdateOrderMenuItemStatus(boothId, orderId, menuItem.MenuItemId), Times.Once);
            _mockUow.Verify(x => x.CommitAsync(CancellationToken.None), Times.Once);
            _mockMediator.Verify(x => x.Publish(It.IsAny<MenuItemCompletedNotification>(), CancellationToken.None), Times.Once);

        }
    }
}
