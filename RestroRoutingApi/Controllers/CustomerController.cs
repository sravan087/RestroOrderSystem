using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestroRouting.Service.Orchestration;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestroRoutingApi.Controllers
{
    [Route("api/customer")]
    public class CustomerController : ApiController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IHttpActionResult> PlaceOrder([FromBody] PlaceCustomerOrderCommand placeCustomerOrderCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(placeCustomerOrderCommand, cancellationToken);

            return Ok(); 

        }
    }
}
