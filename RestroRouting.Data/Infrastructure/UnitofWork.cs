using RestroRouting.Data.Infrastructure.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Data.Infrastructure
{
    public class UnitofWork : IUnitofWork
    {
        private readonly RestroRoutingContext _restroRoutingContext;

        public UnitofWork(RestroRoutingContext restroRoutingContext)
        {
            _restroRoutingContext = restroRoutingContext;
        }
        public async Task<bool> CommitAsync(CancellationToken cancellationToken)
        {
            var result = await _restroRoutingContext.SaveChangesAsync(cancellationToken);
            return result != 0;
        }
    }
}
