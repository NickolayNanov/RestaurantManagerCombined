using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Requests;
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
        /// Create an employee.
        /// </summary>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(CreateEmployeeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateEmployeeResponse>> Create([FromForm] CreateEmployeeFormRequest request)
        {
            var command = new CreateEmployeeCommand
            {
                Name = request.Name,
                Email = request.Email,
                Position = request.Position,
                EmploymentType = request.EmploymentType,
                Status = request.Status,
                Salary = request.Salary,
                PhoneNumber = request.PhoneNumber,
                RestaurantId = request.RestaurantId,
                Image = request.Image.ToUploadFile()
            };

            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Full update (replace) of an employee.
        /// </summary>
        [HttpPut]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] UpdateEmployeeFormRequest request)
        {
            var command = new UpdateEmployeeCommand
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Position = request.Position,
                EmploymentType = request.EmploymentType,
                Status = request.Status,
                Salary = request.Salary,
                PhoneNumber = request.PhoneNumber,
                Image = request.Image.ToUploadFile()
            };

            await mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete an employee by id.
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
