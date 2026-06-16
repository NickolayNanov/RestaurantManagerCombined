using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Api.Requests;
using RestaurantManager.Application.Handlers.UserDetails.GetByUserId;
using RestaurantManager.Application.Handlers.UserDetails.Update;

namespace RestaurantManager.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/user-details")]
    [Produces("application/json")]
    public class UserDetailsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(GetProfileDetailsByUserIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetProfileDetailsByUserIdResponse>> GetByUserId(string userId)
        {
            var result = await mediator.Send(new GetProfileDetailsByUserIdQuery(userId));
            return Ok(result);
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(UpdateProfileDetailsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UpdateProfileDetailsResponse>> Update([FromForm] UpdateUserDetailsFormRequest request)
        {
            var result = await mediator.Send(new UpdateProfileDetailsCommand
            {
                UserId = request.UserId,
                FirstName = request.FirstName,
                Surname = request.Surname,
                LastName = request.LastName,
                CompanyName = request.CompanyName,
                PhoneNumber = request.PhoneNumber,
                Image = request.Image.ToUploadFile()
            });

            return Ok(result);
        }
    }
}
