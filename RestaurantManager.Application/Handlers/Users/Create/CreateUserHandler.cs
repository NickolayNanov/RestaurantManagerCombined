using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Users.Create
{
    internal class CreateUserHandler(
        ILogger<CreateUserHandler> logger,
        UserManager<ApplicationUser> userManager
        ) : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var creationResult = await userManager.CreateAsync(user, request.Password);
            logger.LogInformation("Created new user with username: {Username}", request.Username);

            if (!creationResult.Succeeded)
            {
                logger.LogError("Created failed to be created");
                throw new InvalidOperationException("Failed to create user: " + string.Join(", ", creationResult.Errors.Select(e => e.Description)));
            }

            await userManager.AddToRoleAsync(user, "Owner");

            return new CreateUserResponse { Id = user.Id, Username = user.UserName };
        }
    }
}
