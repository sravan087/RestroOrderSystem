using MediatR;
using RestroRouting.Data.BoothRepository.Interfaces;
using RestroRouting.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Service.Queries
{
    public class GetBoothOrderQueryHandler : IRequestHandler<GetBoothOrderQuery, Booth>
    {
        private readonly IBoothData _boothData;

        public GetBoothOrderQueryHandler(IBoothData boothData)
        {
            _boothData = boothData;
        }

        public async Task<Booth> Handle(GetBoothOrderQuery request, CancellationToken cancellationToken)
        {
            return await _boothData.Get(request.BoothId);
        }
    }
}
