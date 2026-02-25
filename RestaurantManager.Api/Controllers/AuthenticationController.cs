using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] AuthQuery request)
        {
            var res = await this.mediator.Send(request);
            return Ok(res);
        }
    }
}
