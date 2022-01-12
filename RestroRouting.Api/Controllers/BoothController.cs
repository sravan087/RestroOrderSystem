using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestroRouting.Service.Orchestration;
using RestroRouting.Service.Queries;

namespace RestroRoutingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BoothController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoothController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<IActionResult> Get()
        {
            return Ok(new { Hello = "Hello" });
        }

        [HttpPost("placeorder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceBoothOrderCommand placeBoothOrderCommand, CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _mediator.Send(placeBoothOrderCommand, cancellationToken));
            }
            catch(Exception ex)
            {
                 return BadRequest( new ProblemDetails { Status = 500, Title = "Error while placing order", Detail = JsonConvert.SerializeObject(ex) });
            }
        }


        [HttpGet("{boothId}")]
        public async Task<IActionResult> Details([FromRoute] Guid boothId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetBoothOrderQuery(boothId)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails { Status = 500, Title = "Error while fetching order detials", Detail = JsonConvert.SerializeObject(ex) });
            }
        }
    }


}
