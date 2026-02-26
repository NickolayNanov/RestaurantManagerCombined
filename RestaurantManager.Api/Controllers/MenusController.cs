using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        /// <summary>
        /// List menus.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetManyMenusResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetManyMenusResponse>>> GetAll()
        {
            var menus = await mediator.Send(new ListAllMenusQuery());
            return Ok(menus);
        }

        /// <summary>
        /// Get a menu by id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetMenuByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetMenuByIdResponse>> GetById(Guid id)
        {
            var menu = await mediator.Send(new GetMenuByIdQuery(id));
            return Ok(menu);
        }

        /// <summary>
        /// Create a menu.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateMenuResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateMenuResponse>> Create([FromBody] CreateMenuCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Full update (replace) of a menu.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateMenuCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a menu by id.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            await mediator.Send(new DeleteMenuCommand(id));
            return NoContent();
        }
    }
}
