using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Requests;
using RestaurantManager.Application.Handlers.MenuItems.Create;
using RestaurantManager.Application.Handlers.MenuItems.Delete;
using RestaurantManager.Application.Handlers.MenuItems.GetById;
using RestaurantManager.Application.Handlers.MenuItems.GetByMenu;
using RestaurantManager.Application.Handlers.MenuItems.GetMany;
using RestaurantManager.Application.Handlers.MenuItems.Update;
using RestaurantManager.Application.Handlers.Menus.GetById;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/menu-items")]
    [Produces("application/json")]
    public class MenuItemsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetManyMenuItemsResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetManyMenuItemsResponse>>> GetAll()
        {
            var menus = await mediator.Send(new ListAllMenuItemsQuery());
            return Ok(menus);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetMenuItemByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetMenuItemByIdResponse>> GetById(Guid id)
        {
            var menu = await mediator.Send(new GetMenuItemByIdQuery(id));
            return Ok(menu);
        }

        [HttpGet("by-menu/{menuId:guid}")]
        [ProducesResponseType(typeof(GetMenuByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetMenuByIdResponse>> GetMyMenu(Guid menuId)
        {
            var menu = await mediator.Send(new GetMenuItemsByMenuQuery(menuId));
            return Ok(menu);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(CreateMenuItemResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateMenuItemResponse>> Create([FromForm] CreateMenuItemFormRequest request, CancellationToken ct)
        {
            var command = new CreateMenuItemCommand
            {
                Name = request.Name,
                Price = request.Price,
                IsActive = request.IsActive,
                MenuId = request.MenuId,
                CategoryId = request.CategoryId,
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
        public async Task<IActionResult> Update([FromForm] UpdateMenuItemFormRequest request, CancellationToken ct)
        {
            var command = new UpdateMenuItemCommand
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                IsActive = request.IsActive,
                CategoryId = request.CategoryId,
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
            await mediator.Send(new DeleteMenuItemCommand(id), ct);
            return NoContent();
        }
    }
}
