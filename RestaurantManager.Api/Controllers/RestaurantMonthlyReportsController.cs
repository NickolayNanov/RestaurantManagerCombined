using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.RestaurantMonthlyReports;
using RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Create;
using RestaurantManager.Application.Handlers.RestaurantMonthlyReports.GetByMonth;
using RestaurantManager.Application.Handlers.RestaurantMonthlyReports.ListByRestaurant;
using RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Update;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/restaurants/{restaurantId:guid}/monthly-reports")]
    [Produces("application/json")]
    public class RestaurantMonthlyReportsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ListRestaurantMonthlyReportsByRestaurantResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<ListRestaurantMonthlyReportsByRestaurantResponse>> ListByRestaurant(Guid restaurantId)
        {
            var result = await mediator.Send(new ListRestaurantMonthlyReportsByRestaurantQuery(restaurantId));
            return Ok(result);
        }

        [HttpGet("{year:int}/{month:int}")]
        [ProducesResponseType(typeof(RestaurantMonthlyReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RestaurantMonthlyReportResponse>> GetByMonth(Guid restaurantId, int year, int month)
        {
            var result = await mediator.Send(new GetRestaurantMonthlyReportByMonthQuery(restaurantId, year, month));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RestaurantMonthlyReportResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RestaurantMonthlyReportResponse>> Create(Guid restaurantId, [FromBody] CreateRestaurantMonthlyReportCommand request)
        {
            var command = request with { RestaurantId = restaurantId };
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByMonth), new { restaurantId, year = result.Year, month = result.Month }, result);
        }

        [HttpPut("{year:int}/{month:int}")]
        [ProducesResponseType(typeof(RestaurantMonthlyReportResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RestaurantMonthlyReportResponse>> Update(
            Guid restaurantId,
            int year,
            int month,
            [FromBody] UpdateRestaurantMonthlyReportCommand request)
        {
            var command = request with { RestaurantId = restaurantId, Year = year, Month = month };
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
