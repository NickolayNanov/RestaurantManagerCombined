using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Application.Handlers.Restaurants.Delete;
using RestaurantManager.Application.Handlers.Restaurants.GetById;
using RestaurantManager.Application.Handlers.Restaurants.GetMany;
using RestaurantManager.Application.Handlers.Restaurants.Update;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/restaurants")]
    [Produces("application/json")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public RestaurantsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this._mapper = mapper;
        }

        /// <summary>
        /// List restaurants (supports basic pagination via skip/take).
        /// </summary>
        /// <remarks>
        /// GET /api/restaurants?skip=0&take=20
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetManyRestaurantsResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetManyRestaurantsResponse>>> GetAll()
        {
            var items = await mediator.Send(new ListAllRestaurantsQuery());
            return Ok(items);
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
        /// Create a restaurant.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateRestaurantResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateRestaurantResponse>> Create([FromBody] CreateRestaurantCommand dto)
        {
            var result = await mediator.Send(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Full update (replace) of a restaurant.
        /// </summary>
        [HttpPut]
        [Authorize] // optionally: [Authorize(Roles = "Owner,Admin")]
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
        [Authorize] // optionally: [Authorize(Roles = "Owner,Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            await mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();
        }
    }
}
