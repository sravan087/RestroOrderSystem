using MediatR;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Domain;
using RestroRouting.Service.Logic.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Orchestration
{
    public class OrderPlacedNotificationHandler : IRequestHandler<OrderPlacedNotification>
    {
        private readonly IMenuItemFactoryProcesser _menuItemFactoryProcesser;
        private readonly IBoothService _boothService;
        private readonly IUnitofWork _uow;

        public OrderPlacedNotificationHandler(IMenuItemFactoryProcesser menuItemFactoryProcesser, IBoothService boothService, IUnitofWork unitofWork)
        {
            _menuItemFactoryProcesser = menuItemFactoryProcesser;
            _boothService = boothService;
            _uow = unitofWork;
        }

        public async Task<Unit> Handle(OrderPlacedNotification request, CancellationToken cancellationToken)
        {
            await _boothService.UpdateOrderStatus(request.BoothId, request.OrderId, OrderStatus.InProgress);

            await _uow.CommitAsync(cancellationToken);

            await _menuItemFactoryProcesser.ProcessMenuItems(request.BoothId, request.OrderId, request.MenuItems, cancellationToken);

           

            return Unit.Value;
        }



    }
}
