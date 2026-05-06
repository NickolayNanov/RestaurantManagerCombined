using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Requests;
using RestaurantManager.Application.Handlers.Menus.Create;
using RestaurantManager.Application.Handlers.Menus.Delete;
using RestaurantManager.Application.Handlers.Menus.GetById;
using RestaurantManager.Application.Handlers.Menus.GetMany;
using RestaurantManager.Application.Handlers.Menus.Update;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/menus")]
    [Produces("application/json")]
    public class MenusController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetManyMenusResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetManyMenusResponse>>> GetAll()
        {
            var menus = await mediator.Send(new ListAllMenusQuery());
            return Ok(menus);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetMenuByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetMenuByIdResponse>> GetById(Guid id)
        {
            var menu = await mediator.Send(new GetMenuByIdQuery(id));
            return Ok(menu);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(CreateMenuResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateMenuResponse>> Create([FromForm] CreateMenuFormRequest request, CancellationToken ct)
        {
            var command = new CreateMenuCommand
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                Type = request.Type,
                RestaurantId = request.RestaurantId,
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
        public async Task<IActionResult> Update([FromForm] UpdateMenuFormRequest request, CancellationToken ct)
        {
            var command = new UpdateMenuCommand
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                Type = request.Type,
                RestaurantId = request.RestaurantId,
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
            await mediator.Send(new DeleteMenuCommand(id), ct);
            return NoContent();
        }
    }
}
