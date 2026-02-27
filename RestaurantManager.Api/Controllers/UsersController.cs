using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Application.Handlers.Users.Create;
using RestaurantManager.Application.Handlers.Users.GetById;
using RestaurantManager.Application.Handlers.Users.Me;
using RestaurantManager.Application.Handlers.Users.UserInfo;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateRestaurantResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateRestaurantResponse>> Create([FromBody] CreateUserCommand dto)
        {
            var result = await mediator.Send(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreateRestaurantResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetUserByIdResponse>> GetById(string id)
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("user-info")]
        [ProducesResponseType(typeof(UserInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserInfoResponse>> UserInfo()
        {
            var result = await mediator.Send(new UserInfoResponseQuery());
            return Ok(result);
        }
    }
}
