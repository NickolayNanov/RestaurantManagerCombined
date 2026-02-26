using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Categories.GetMany;
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
    }
}
