using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.UserDetails.Update
{
    internal class UpdateProfileDetailsHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        IImageUploadService imageUploadService,
        ILogger<UpdateProfileDetailsHandler> logger,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<UpdateProfileDetailsCommand, UpdateProfileDetailsResponse>
    {
        public async Task<UpdateProfileDetailsResponse> Handle(UpdateProfileDetailsCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.ProfileDetails
                .FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            if (entity is null)
            {
                logger.LogError("Could not find profile details for user id: {UserId}", request.UserId);
                throw new ResourceNotFoundException(nameof(ProfileDetails), $"No profile details for user id: {request.UserId}");
            }

            entity.FirstName = Normalize(request.FirstName);
            entity.Surname = Normalize(request.Surname);
            entity.LastName = Normalize(request.LastName);
            entity.CompanyName = Normalize(request.CompanyName);
            entity.PhoneNumber = Normalize(request.PhoneNumber);
            entity.ProfilePictureUrl = request.Image is null
                ? entity.ProfilePictureUrl
                : await imageUploadService.UploadAsync(request.Image, ImageUploadFolders.ProfileDetails, cancellationToken);
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = currentUserService.UserId;

            logger.LogInformation("Updated profile details for user id: {UserId}", request.UserId);

            return mapper.Map<UpdateProfileDetailsResponse>(entity);
        }

        private static string Normalize(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }
}
