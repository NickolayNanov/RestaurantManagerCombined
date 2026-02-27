using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Application.Handlers.Restaurants.Delete;
using RestaurantManager.Application.Handlers.Restaurants.GetAllRestaurantInfos;
using RestaurantManager.Application.Handlers.Restaurants.GetById;
using RestaurantManager.Application.Handlers.Restaurants.GetOwnersRestaurants;
using RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo;
using RestaurantManager.Application.Handlers.Restaurants.Update;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/restaurants")]
    [Produces("application/json")]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// List restaurants (supports basic pagination via skip/take).
        /// </summary>
        /// <remarks>
        /// GET /api/restaurants?skip=0&take=20
        /// </remarks>
        [HttpGet("info")]
        [ProducesResponseType(typeof(IEnumerable<GetAllRestaurantInfosResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetAllRestaurantInfosResponse>>> GetAllRestaurantInfos()
        {
            var items = await mediator.Send(new GetAllRestaurantInfosQuery());
            return Ok(items);
        }

        /// <summary>
        /// Get a restaurant's info by id.
        /// </summary>
        [HttpGet("info/{id:guid}")]
        [ProducesResponseType(typeof(GetRestaurantInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetRestaurantInfoResponse>> GetRestaurantInfo(Guid id)
        {
            var entity = await mediator.Send(new GetRestaurantInfoQuery(id));
            return Ok(entity);
        }

        /// <summary>
        /// Get a restaurant by id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetRestaurantByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetRestaurantByIdResponse>> GetById(Guid id)
        {
            var entity = await mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(entity);
        }

        /// <summary>
        /// Get a restaurant by id.
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(typeof(GetOwnersRestaurantsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetOwnersRestaurantsResponse>> GetOwnersRestaurants()
        {
            var entity = await mediator.Send(new GetOwnersRestaurantsQuery());
            return Ok(entity);
        }

        /// <summary>
        /// Create a restaurant.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateRestaurantResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateRestaurantResponse>> Create([FromBody] CreateRestaurantCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Full update (replace) of a restaurant.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateRestaurantCommand dto)
        {
            await mediator.Send(dto);
            return NoContent();
        }

        /// <summary>
        /// Delete a restaurant.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }
    }
}
