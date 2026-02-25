using MediatR;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Handlers.Restaurants.Create;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Restaurants.Delete
{
    public class DeleteRestaurantCommandHandler(
        ILogger<CreateRestaurantCommandHandler> logger,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<DeleteRestaurantCommand, DeleteRestaurantResponse>
    {
        public async Task<DeleteRestaurantResponse> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantManagerDbContext.Restaurants.FindAsync(request.Id, cancellationToken);

            if (restaurant is null)
            {
                logger.LogWarning("Restaurant with id {Id} not found", request.Id);
                throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id {request.Id} was not found when trying to delete.");
            }

            restaurantManagerDbContext.Restaurants.Remove(restaurant);

            return new DeleteRestaurantResponse(true);
        }
    }
}
