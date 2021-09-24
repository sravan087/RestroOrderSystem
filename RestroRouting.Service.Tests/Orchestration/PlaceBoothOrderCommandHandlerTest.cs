using FluentAssertions;
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
    public class PlaceBoothOrderCommandHandlerTest 
    {
        private readonly Mock<IBoothService> _mockBoothService;
        private readonly Mock<IUnitofWork> _mockUow;
        private readonly Mock<IMediator> _mockMediator;

        public PlaceBoothOrderCommandHandlerTest()
        {
            _mockBoothService = new Mock<IBoothService>();
            _mockUow = new Mock<IUnitofWork>();
            _mockMediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Place_Booth_Order_Command_Handler_Test()
        {
            // Arrange
            var placeBoothOrderCommand = new PlaceBoothOrderCommand(1, "Sam", new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 1, 1.5M, MenuItemType.Desert) });
        
            var booth = new Booth(1, "sam");
            var menuItems = new List<MenuItemData> { new MenuItemData(Guid.NewGuid(), 1, 1.5M, MenuItemType.Desert) };
            booth.PlaceOrder(menuItems);
            _mockBoothService.Setup(x => x.PlaceOrder(placeBoothOrderCommand)).ReturnsAsync(booth);


            // Act
            var handler = new PlaceBoothOrderCommandHandler(_mockMediator.Object, _mockBoothService.Object, _mockUow.Object);
            var result = await handler.Handle(placeBoothOrderCommand, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();
            _mockUow.Verify(x => x.CommitAsync(CancellationToken.None), Times.Once);
            _mockMediator.Verify(x => x.Send(It.IsAny<OrderPlacedNotification>(), CancellationToken.None), Times.Once);
        }
    }
}
