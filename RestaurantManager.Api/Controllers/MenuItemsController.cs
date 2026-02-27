using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// List menu items.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetManyMenuItemsResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetManyMenuItemsResponse>>> GetAll()
        {
            var menus = await mediator.Send(new ListAllMenuItemsQuery());
            return Ok(menus);
        }

        /// <summary>
        /// Get a menu item by id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetMenuItemByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetMenuItemByIdResponse>> GetById(Guid id)
        {
            var menu = await mediator.Send(new GetMenuItemByIdQuery(id));
            return Ok(menu);
        }

        /// <summary>
        /// Get a menu items by menu id.
        /// </summary>
        [HttpGet("by-menu/{menuId:guid}")]
        [ProducesResponseType(typeof(GetMenuByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetMenuByIdResponse>> GetMyMenu(Guid menuId)
        {
            var menu = await mediator.Send(new GetMenuItemsByMenuQuery(menuId));
            return Ok(menu);
        }

        /// <summary>
        /// Create a menu item.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateMenuItemResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateMenuItemResponse>> Create([FromBody] CreateMenuItemCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Full update (replace) of a menu item.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateMenuItemCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a menu item by id.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            await mediator.Send(new DeleteMenuItemCommand(id));
            return NoContent();
        }
    }
}
