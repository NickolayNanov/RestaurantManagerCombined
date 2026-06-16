using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Handlers.UserDetails.Create;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Users.Create
{
    internal class CreateUserHandler(
        ILogger<CreateUserHandler> logger,
        IMediator mediator,
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
                logger.LogError("User failed to be created");
                throw new InvalidOperationException("Failed to create user: " + string.Join(", ", creationResult.Errors.Select(e => e.Description)));
            }

            var roleResult = await userManager.AddToRoleAsync(user, "Owner");
            if (!roleResult.Succeeded)
            {
                logger.LogError("Failed to assign Owner role to user: {Username}", request.Username);
                throw new InvalidOperationException("Failed to assign user role: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            await mediator.Send(new CreateProfileDetailsCommand { UserId = user.Id }, cancellationToken);

            return new CreateUserResponse { Id = user.Id, Username = user.UserName };
        }
    }
}
