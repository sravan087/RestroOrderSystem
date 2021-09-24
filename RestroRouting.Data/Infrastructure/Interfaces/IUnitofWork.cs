using System.Threading;
using System.Threading.Tasks;

namespace RestroRouting.Data.Infrastructure.Interfaces
{
    public interface IUnitofWork
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken);
    }
}
