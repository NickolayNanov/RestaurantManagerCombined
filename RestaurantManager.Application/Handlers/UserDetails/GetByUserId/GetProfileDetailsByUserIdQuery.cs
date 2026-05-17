using MediatR;

namespace RestaurantManager.Application.Handlers.UserDetails.GetByUserId
{
    public record GetProfileDetailsByUserIdQuery(string UserId) : IRequest<GetProfileDetailsByUserIdResponse>;
}
