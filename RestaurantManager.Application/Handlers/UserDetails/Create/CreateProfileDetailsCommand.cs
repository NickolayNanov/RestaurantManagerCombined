using MediatR;
using RestaurantManager.Application.Handlers.UserDetails;

namespace RestaurantManager.Application.Handlers.UserDetails.Create
{
    public record CreateProfileDetailsCommand : ProfileDetailsBase, IRequest<CreateProfileDetailsResponse>
    {
        public string UserId { get; set; }
    }
}
