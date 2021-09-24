using RestroRouting.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RestroRouting.Data.BoothRepository.Interfaces
{
    public interface IBoothData
    {
        Task Save(Booth customer);

        Task<Booth> Get(Guid guid);

         void Update(Booth booth);
    }
}
