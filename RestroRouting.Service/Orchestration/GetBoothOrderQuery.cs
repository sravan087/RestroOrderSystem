using MediatR;
using RestroRouting.Domain.Entities;
using System;

namespace RestroRouting.Service.Queries
{
    public class GetBoothOrderQuery : IRequest<Booth>
    {
        public Guid BoothId { get; }

        public GetBoothOrderQuery(Guid boothId )
        {
            BoothId = boothId;
        }
    }
}
