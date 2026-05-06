using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Restaurants.Update
{
    public record UpdateRestaurantCommandHandler(
        IMapper mapper,
        ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantManagerDbContext restaurantManagerDbContext,
        ICurrentUserService currentUserService,
        IImageUploadService imageUploadService) : IRequestHandler<UpdateRestaurantCommand, UpdateRestaurantResponse>
    {
        public async Task<UpdateRestaurantResponse> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var dbRecord = await restaurantManagerDbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id {request.Id} was not found when trying to update.");

            dbRecord.Name = request.Name;
            dbRecord.Description = request.Description;
            dbRecord.Location = request.Location;
            dbRecord.Cuisine = request.Cuisine;
            dbRecord.Status = request.Status;
            dbRecord.UpdatedBy = currentUserService.UserId;
            dbRecord.UpdatedAt = DateTime.UtcNow;

            if (request.Image is not null)
            {
                dbRecord.ImgUrl = await imageUploadService.UploadAsync(
                    request.Image,
                    ImageUploadFolders.Restaurants,
                    cancellationToken);
            }

            restaurantManagerDbContext.Restaurants.Update(dbRecord);

            logger.LogInformation($"Updated restaurant with id {dbRecord.Id}.");

            var response = mapper.Map<UpdateRestaurantResponse>(dbRecord);

            return response;
        }
    }
}
