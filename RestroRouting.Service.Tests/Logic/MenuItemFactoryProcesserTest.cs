using Autofac.Features.Indexed;
using Moq;
using NUnit.Framework;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Domain;
using RestroRouting.Domain.Entities;
using RestroRouting.Service.Logic;
using RestroRouting.Service.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Tests.Logic
{
    public class MenuItemFactoryProcesserTest 
    {
        private readonly Mock<IIndex<MenuItemType, IMenuItemProcesser>> _mockMenuItemIndexProcesser;
        private readonly Mock<IUnitofWork> _mockUow;

        public MenuItemFactoryProcesserTest()
        {
            _mockMenuItemIndexProcesser = new Mock<IIndex<MenuItemType, IMenuItemProcesser>>();
            _mockUow = new Mock<IUnitofWork>();
        }

        [Test]
        public async Task MenuItem_Index_Processer_Test()
        {
            // Arrange
            var boothId = new Guid("b99ca376-2b74-4276-908c-d27243d63ee7");
            var orderId = new Guid("d235b632-16fb-46eb-8a13-4bd3eedfa7d5");
            var menuItems = new List<MenuItemData> { 
                new MenuItemData(new Guid("f1dea6fd-bda0-4e35-bfbd-aef25f14c33b"),1, 2.5M, MenuItemType.Desert),
                    new MenuItemData(new Guid("90baed1a-5b14-4665-a3b1-7b9e475c4162"),1, 2.5M, MenuItemType.Drink)
            };

            _mockMenuItemIndexProcesser.Setup(x => x[It.IsAny<MenuItemType>()].ProcessMenuItem(boothId, orderId, It.IsAny<MenuItemData>(), CancellationToken.None)).ReturnsAsync(true);


            // Act
            IMenuItemFactoryProcesser menuItemFactoryProcesser = new MenuItemFactoryProcesser(_mockMenuItemIndexProcesser.Object);

            await menuItemFactoryProcesser.ProcessMenuItems(boothId, orderId, menuItems, CancellationToken.None);

            // Assert
            _mockMenuItemIndexProcesser.Verify(x => x[It.IsAny<MenuItemType>()].ProcessMenuItem(boothId, orderId, It.IsAny<MenuItemData>(), CancellationToken.None), Times.AtMost(2));

        }
    }
}
