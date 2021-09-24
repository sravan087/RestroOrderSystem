using MediatR;
using Moq;
using NUnit.Framework;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Domain;
using RestroRouting.Domain.Entities;
using RestroRouting.Service.Logic.Interfaces;
using RestroRouting.Service.Orchestration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Tests.Orchestration
{
    [TestFixture]
    public class OrderPlacedNotificationHandlerTest
    {
        private readonly Mock<IMenuItemFactoryProcesser> _mockMenuItemFactoryProcesser;
        private readonly Mock<IBoothService> _mockBoothService;
        private readonly Mock<IUnitofWork> _mockUow;
        private readonly Mock<IMediator> _mockMediator;

        public OrderPlacedNotificationHandlerTest()
        {
            _mockMenuItemFactoryProcesser = new Mock<IMenuItemFactoryProcesser>();
            _mockBoothService = new Mock<IBoothService>();
            _mockUow = new Mock<IUnitofWork>();
            _mockMediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Order_Placed_Notification_Handler_Test()
        {
            // Arrange
            var orderPlacedNotification = new OrderPlacedNotification(new Guid("b99ca376-2b74-4276-908c-d27243d63ee7"), new Guid("b99ca376-2b74-4276-908c-d27243d63ee7"), new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 1, 1.5M, MenuItemType.Desert) });

           
            


            // Act
            var handler = new OrderPlacedNotificationHandler(_mockMenuItemFactoryProcesser.Object, _mockBoothService.Object, _mockUow.Object);
            await handler.Handle(orderPlacedNotification, CancellationToken.None);

            // Assert
            _mockBoothService.Verify(x => x.UpdateOrderStatus(It.IsAny<Guid>(), It.IsAny<Guid>(), OrderStatus.InProgress), Times.Once);
            _mockUow.Verify(x => x.CommitAsync(CancellationToken.None), Times.Once);
            _mockMenuItemFactoryProcesser.Verify(x => x.ProcessMenuItems(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<List<MenuItemData>>(), CancellationToken.None), Times.Once);

        }
    }
}
