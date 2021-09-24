using MediatR;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Service.Logic.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Orchestration
{
    public class PlaceBoothOrderCommandHandler : IRequestHandler<PlaceBoothOrderCommand, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IBoothService _boothService;
        private readonly IUnitofWork _uow;

        public PlaceBoothOrderCommandHandler(IMediator mediator, IBoothService boothService, IUnitofWork unitofWork)
        {
            _mediator = mediator;
            _boothService = boothService;
            _uow = unitofWork;

        }
        public async Task<Guid> Handle(PlaceBoothOrderCommand placeBoothOrderCommand, CancellationToken cancellationToken)
        {
            var booth = await _boothService.PlaceOrder(placeBoothOrderCommand);

            await _uow.CommitAsync(cancellationToken);

           

            booth.Orders.ForEach(o =>
            {
               _ = _mediator.Send(new OrderPlacedNotification(booth.BoothId, o.OrderId, placeBoothOrderCommand.MenuItems), cancellationToken);
            });



            return booth.BoothId;


        }
    }
}
