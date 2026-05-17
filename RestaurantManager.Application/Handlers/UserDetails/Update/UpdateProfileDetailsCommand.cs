using MediatR;
using RestaurantManager.Application.Handlers.UserDetails;
using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Handlers.UserDetails.Update
{
    public record UpdateProfileDetailsCommand : ProfileDetailsBase, IRequest<UpdateProfileDetailsResponse>
    {
        public string UserId { get; set; }

        public UploadFile Image { get; set; }
    }
}
