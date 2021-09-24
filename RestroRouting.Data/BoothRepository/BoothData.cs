using RestroRouting.Data.BoothRepository.Interfaces;
using RestroRouting.Data.Infrastructure;
using RestroRouting.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RestroRouting.Data.BoothRepository
{
    public class BoothData : IBoothData
    {
        private readonly RestroRoutingContext _restroRoutingContext;

        public BoothData(RestroRoutingContext restroRoutingContext)
        {
            _restroRoutingContext = restroRoutingContext;
        }
        public async Task Save(Booth booth)
        {
            await _restroRoutingContext.Booths.AddAsync(booth);
        }

        public async Task<Booth> Get(Guid guid)
        {
            return await _restroRoutingContext.Booths.FindAsync(guid);
        }

        public void Update(Booth booth)
        {
            _restroRoutingContext.Booths.Update(booth);
        }
    }
}
