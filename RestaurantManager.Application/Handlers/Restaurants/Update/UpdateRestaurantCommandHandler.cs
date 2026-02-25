using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Restaurants.Update
{
    public record UpdateRestaurantCommandHandler(
        IMapper mapper,
        ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantManagerDbContext restaurantManagerDbContext,
        ICurrentUserService currentUserService) : IRequestHandler<UpdateRestaurantCommand, UpdateRestaurantResponse>
    {
        public async Task<UpdateRestaurantResponse> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var dbRecord = await restaurantManagerDbContext.Restaurants.FindAsync(request.Id, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id {request.Id} was not found when trying to update.");

            var entity = mapper.Map<Restaurant>(request);

            entity.UpdatedBy = currentUserService.UserId;
            entity.UpdatedAt = DateTime.UtcNow;

            restaurantManagerDbContext.Restaurants.Update(entity);

            logger.LogInformation($"Updated restaurant with id {entity.Id}.");

            var response = mapper.Map<UpdateRestaurantResponse>(entity);

            return response;
        }
    }
}
