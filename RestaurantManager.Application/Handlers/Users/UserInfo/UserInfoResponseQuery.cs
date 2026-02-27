using MediatR;

namespace RestaurantManager.Application.Handlers.Users.UserInfo
{
    public record UserInfoResponseQuery() : IRequest<UserInfoResponse>;
}
