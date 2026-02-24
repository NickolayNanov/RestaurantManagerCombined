using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            var entity = mapper.Map<Restaurant>(request);

            entity.UpdatedBy = currentUserService.UserId;
            entity.UpdatedAt = DateTime.UtcNow;

            restaurantManagerDbContext.Restaurants.Update(entity);
            logger.LogInformation($"Updated restaurant with id {entity.Id}.");
        }
    }
}
