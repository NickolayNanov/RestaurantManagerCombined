using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Categories.Create;
using RestaurantManager.Application.Handlers.Categories.Delete;
using RestaurantManager.Application.Handlers.Categories.GetById;
using RestaurantManager.Application.Handlers.Categories.GetMany;
using RestaurantManager.Application.Handlers.Categories.Update;
using RestaurantManager.Application.Handlers.MenuItems.GetMany;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/categories")]
    [Produces("application/json")]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// List categories.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetManyMenuItemsResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetManyMenuItemsResponse>>> GetAll()
        {
            var response = await mediator.Send(new ListAllCategoriesQuery());
            return Ok(response);
        }

        /// <summary>
        /// Gets a category by id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IEnumerable<GetManyMenuItemsResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetManyMenuItemsResponse>>> GetById(Guid id)
        {
            var response = await mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(response);
        }

        /// <summary>
        /// Create a category.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateCategoryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCategoryResponse>> Create([FromBody] CreateCategoryCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Full update (replace) of a category.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a category by id.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            await mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}
