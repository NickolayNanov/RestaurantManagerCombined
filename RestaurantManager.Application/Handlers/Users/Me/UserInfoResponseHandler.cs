using MediatR;
using Microsoft.AspNetCore.Identity;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Users.Me
{
    internal class UserInfoResponseHandler(
        ICurrentUserService currentUserService,
        UserManager<ApplicationUser> userManager
        ) : IRequestHandler<UserInfoResponseQuery, UserInfoResponse>
    {
        public async Task<UserInfoResponse> Handle(UserInfoResponseQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(currentUserService.UserId);
            var roles = await userManager.GetRolesAsync(user);

            return new UserInfoResponse(user.Id, user.UserName, user.Email, roles);
        }
    }
}
