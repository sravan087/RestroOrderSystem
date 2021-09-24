using Moq;
using NUnit.Framework;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Service.Logic.Interfaces;
using RestroRouting.Service.Orchestration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Tests.Orchestration
{
    [TestFixture]
    public class MenuItemCompletedNotificationHandlerTest
    {
        private readonly Mock<IBoothService> _mockBoothService;
        private readonly Mock<IUnitofWork> _mockUow;

        public MenuItemCompletedNotificationHandlerTest()
        {
            _mockBoothService = new Mock<IBoothService>();
            _mockUow = new Mock<IUnitofWork>();
        }


        [Test]
        public async Task Menu_Item_Completed_Handler_Test()
        {
            // Arrange
            var menuItemCompletedNotification = new MenuItemCompletedNotification(new Guid("b99ca376-2b74-4276-908c-d27243d63ee7"),
                new Guid("b99ca376-2b74-4276-908c-d27243d63ee7"),
                new Guid("f1dea6fd-bda0-4e35-bfbd-aef25f14c33b"));


            // Act
            var handler = new MenuItemCompletedNotificationHandler(_mockBoothService.Object, _mockUow.Object);
             await handler.Handle(menuItemCompletedNotification, CancellationToken.None);

            // Assert
            _mockUow.Verify(x => x.CommitAsync(CancellationToken.None), Times.Once);
            _mockBoothService.Verify(x => x.UpdateOrderStatusToComplete(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }
    }
}
