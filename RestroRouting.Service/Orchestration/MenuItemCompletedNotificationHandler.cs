using MediatR;
using RestroRouting.Data.Infrastructure.Interfaces;
using RestroRouting.Service.Logic.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Orchestration
{
    public class MenuItemCompletedNotificationHandler : INotificationHandler<MenuItemCompletedNotification>
    {
        private readonly IBoothService _boothService;
        private readonly IUnitofWork _uow;

        public MenuItemCompletedNotificationHandler(IBoothService boothService, IUnitofWork unitofWork)
        {
            _boothService = boothService;
            _uow = unitofWork;
        }


        public async Task Handle(MenuItemCompletedNotification notification, CancellationToken cancellationToken)
        {
            await _boothService.UpdateOrderStatusToComplete(notification.BoothId, notification.OrderId);
            await _uow.CommitAsync(cancellationToken);
        }
    }
}
