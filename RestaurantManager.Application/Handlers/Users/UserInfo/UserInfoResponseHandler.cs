using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Users.UserInfo
{
    internal class UserInfoResponseHandler(
        ICurrentUserService currentUserService,
        UserManager<ApplicationUser> userManager
        ) : IRequestHandler<UserInfoResponseQuery, UserInfoResponse>
    {
        public async Task<UserInfoResponse> Handle(UserInfoResponseQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.Users
                .AsNoTracking()
                .Include(u => u.ProfileDetails)
                .FirstOrDefaultAsync(u => u.Id == currentUserService.UserId, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(ApplicationUser), $"Could not find user with id: {currentUserService.UserId}");

            var roles = await userManager.GetRolesAsync(user);

            return new UserInfoResponse(user.Id, user.UserName, user.Email, roles, user.ProfileDetails?.ProfilePictureUrl);
        }
    }
}
