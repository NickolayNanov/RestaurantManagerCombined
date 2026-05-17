using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.UserDetails.Create
{
    internal class CreateProfileDetailsHandler(
        IMapper mapper,
        UserManager<ApplicationUser> userManager,
        ILogger<CreateProfileDetailsHandler> logger,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<CreateProfileDetailsCommand, CreateProfileDetailsResponse>
    {
        public async Task<CreateProfileDetailsResponse> Handle(CreateProfileDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                logger.LogError("Could not find user for profile details: {UserId}", request.UserId);
                throw new ResourceNotFoundException(nameof(ApplicationUser), $"Could not find user with id: {request.UserId}");
            }

            var alreadyExists = await dbContext.ProfileDetails.AnyAsync(x => x.UserId == request.UserId, cancellationToken);

            if (alreadyExists)
            {
                throw new InvalidOperationException($"Profile details for user {request.UserId} already exist.");
            }

            var entity = mapper.Map<ProfileDetails>(request);
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = request.UserId;

            await dbContext.ProfileDetails.AddAsync(entity, cancellationToken);
            logger.LogInformation("Created profile details for user: {UserId}", request.UserId);

            return mapper.Map<CreateProfileDetailsResponse>(entity);
        }
    }
}
