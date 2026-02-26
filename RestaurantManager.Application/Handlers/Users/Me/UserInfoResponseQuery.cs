using MediatR;

namespace RestaurantManager.Application.Handlers.Users.Me
{
    public record UserInfoResponseQuery() : IRequest<UserInfoResponse>;
}
