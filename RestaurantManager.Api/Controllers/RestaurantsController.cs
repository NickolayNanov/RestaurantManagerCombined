using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Requests;
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
        [HttpGet("info")]
        [ProducesResponseType(typeof(IEnumerable<GetAllRestaurantInfosResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetAllRestaurantInfosResponse>>> GetAllRestaurantInfos()
        {
            var items = await mediator.Send(new GetAllRestaurantInfosQuery());
            return Ok(items);
        }

        [HttpGet("info/{id:guid}")]
        [ProducesResponseType(typeof(GetRestaurantInfoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetRestaurantInfoResponse>> GetRestaurantInfo(Guid id)
        {
            var entity = await mediator.Send(new GetRestaurantInfoQuery(id));
            return Ok(entity);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetRestaurantByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetRestaurantByIdResponse>> GetById(Guid id)
        {
            var entity = await mediator.Send(new GetRestaurantByIdQuery(id));
            return Ok(entity);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetOwnersRestaurantsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetOwnersRestaurantsResponse>> GetOwnersRestaurants()
        {
            var entity = await mediator.Send(new GetOwnersRestaurantsQuery());
            return Ok(entity);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(CreateRestaurantResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateRestaurantResponse>> Create([FromForm] CreateRestaurantFormRequest request, CancellationToken ct)
        {
            var command = new CreateRestaurantCommand
            {
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Cuisine = request.Cuisine,
                Status = request.Status,
                Image = request.Image.ToUploadFile()
            };

            var result = await mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] UpdateRestaurantFormRequest request, CancellationToken ct)
        {
            var command = new UpdateRestaurantCommand
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Cuisine = request.Cuisine,
                Status = request.Status,
                OwnerId = request.OwnerId,
                Image = request.Image.ToUploadFile()
            };

            await mediator.Send(command, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            await mediator.Send(new DeleteRestaurantCommand(id), ct);
            return NoContent();
        }
    }
}
