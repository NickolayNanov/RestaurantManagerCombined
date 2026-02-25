using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private const string access_token = "access_token";
        private readonly IMediator mediator;

        public AuthenticationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Login([FromBody] AuthQuery request)
        {
            var res = await this.mediator.Send(request);

            Response.Cookies.Append(access_token, res.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,               // must be true in prod (and for SameSite=None)
                SameSite = SameSiteMode.Lax, // use Lax if SPA+API are same-site; see notes below
                Expires = DateTimeOffset.UtcNow.AddHours(2),
                Path = "/"
            });

            return NoContent();
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(access_token, new CookieOptions { Path = "/" });
            return NoContent();
        }
    }
}
