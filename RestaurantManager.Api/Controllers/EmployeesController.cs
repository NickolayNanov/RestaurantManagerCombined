using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Employees.Create;
using RestaurantManager.Application.Handlers.Employees.Delete;
using RestaurantManager.Application.Handlers.Employees.GetById;
using RestaurantManager.Application.Handlers.Employees.ListByRestaurant;
using RestaurantManager.Application.Handlers.Employees.Update;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/employees")]
    [Produces("application/json")]
    public class EmployeesController(IMediator mediator) : ControllerBase
    {
        /// <summary>
         /// List employees by restaurant id.
         /// </summary>
        [HttpGet("per-restaurant/{restaurantId}")]
        [ProducesResponseType(typeof(ListEmployeesByRestaurantResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ListEmployeesByRestaurantResponse>> ListEmployeesByRestaurant(Guid restaurantId)
        {
            var employees = await mediator.Send(new ListEmployeesByRestaurantQuery(restaurantId));
            return Ok(employees);
        }

        /// <summary>
        /// Get an employee by id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GetEmployeeByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetEmployeeByIdResponse>> GetById(Guid id)
        {
            var employee = await mediator.Send(new GetEmployeeByIdQuery(id));
            return Ok(employee);
        }

        /// <summary>
        /// Create a menu.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateEmployeeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateEmployeeResponse>> Create([FromBody] CreateEmployeeCommand command)
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
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command)
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
            await mediator.Send(new DeleteEmployeeCommand(id));
            return NoContent();
        }
    }
}
