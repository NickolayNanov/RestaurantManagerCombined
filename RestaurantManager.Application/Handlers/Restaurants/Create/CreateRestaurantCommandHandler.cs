using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Restaurants.Create
{
    public class CreateRestaurantCommandHandler(
        IMapper mapper,
        ILogger<CreateRestaurantCommandHandler> logger,
        IRestaurantManagerDbContext restaurantManagerDbContext,
        ICurrentUserService currentUserService) : IRequestHandler<CreateRestaurantCommand, CreateRestaurantResponse>
    {
        public async Task<CreateRestaurantResponse> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = mapper.Map<Restaurant>(request);

            restaurant.CreatedBy = currentUserService.UserId;
            restaurant.CreatedAt = DateTime.UtcNow;

            restaurant.OwnerId = currentUserService.UserId;

            restaurantManagerDbContext.Restaurants.Add(restaurant);

            logger.LogInformation($"Inserted new restaurant with id {restaurant.Id} into the database.");

            var response = mapper.Map<CreateRestaurantResponse>(restaurant);

            return response;
        }
    }
}
