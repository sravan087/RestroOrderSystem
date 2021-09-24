﻿using MediatR;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Domain.Entities;
using RestroRouting.Service.Logic.Interfaces;
using RestroRouting.Service.Orchestration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Logic
{
    public class DesertAreaProcesser : IMenuItemProcesser
    {
        private readonly IBoothService _boothService;
        private readonly IUnitofWork _uow;
        private readonly IMediator _mediator;

        public DesertAreaProcesser(IBoothService boothService, IUnitofWork unitofwork, IMediator mediator)
        {
            _boothService = boothService;
            _uow = unitofwork;
            _mediator = mediator;
        }

        public async Task<bool> ProcessMenuItem(Guid boothId, Guid orderId, MenuItemData menuItem, CancellationToken cancellationToken)
        {

            Thread.Sleep(500);

            await _boothService.UpdateOrderMenuItemStatus(boothId, orderId, menuItem.MenuItemId);
            await _uow.CommitAsync(cancellationToken);
            await _mediator.Publish(new MenuItemCompletedNotification(boothId, orderId, menuItem.MenuItemId),cancellationToken);
            return true;
        }
    }
}
