using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Handlers.Dashboard.PerformanceAnalytics;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/dashboard")]
    [Produces("application/json")]
    public class DashboardController(IMediator mediator) : ControllerBase
    {
        [HttpGet("performance-analytics")]
        [ProducesResponseType(typeof(GetPerformanceAnalyticsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<GetPerformanceAnalyticsResponse>> GetPerformanceAnalytics([FromQuery] int months = 6)
        {
            var result = await mediator.Send(new GetPerformanceAnalyticsQuery(months));
            return Ok(result);
        }
    }
}
