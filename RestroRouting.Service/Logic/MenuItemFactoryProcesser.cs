using Autofac.Features.Indexed;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Domain;
using RestroRouting.Domain.Entities;
using RestroRouting.Service.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Logic
{
    public class MenuItemFactoryProcesser : IMenuItemFactoryProcesser
    {
        private readonly IIndex<MenuItemType, IMenuItemProcesser> _menuItemProcesser;

        public MenuItemFactoryProcesser(IIndex<MenuItemType, IMenuItemProcesser> menuItemProcesser)
        {
            _menuItemProcesser = menuItemProcesser;
        }
        public async Task ProcessMenuItems(Guid boothId, Guid orderId, List<MenuItemData> menuItems, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach(var m in menuItems)
            {
                tasks.Add(_menuItemProcesser[m.MenuItemType].ProcessMenuItem(boothId, orderId, m, cancellationToken));
            }

            //menuItems.ForEach(m =>
            //{
                
            //});
            await Task.WhenAll(tasks);
        }
    }
}
