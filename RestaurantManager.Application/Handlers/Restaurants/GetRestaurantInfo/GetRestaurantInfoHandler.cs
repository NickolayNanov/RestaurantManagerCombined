using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo
{
    internal class GetRestaurantInfoHandler(
        ILogger<GetRestaurantInfoHandler> logger,
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<GetRestaurantInfoQuery, GetRestaurantInfoResponse>
    {
        public async Task<GetRestaurantInfoResponse> Handle(GetRestaurantInfoQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantManagerDbContext.Restaurants
                .AsSplitQuery()
                .AsNoTracking()
                .ProjectTo<GetRestaurantInfoResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.Id == request.Id)
                ?? throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id {request.Id} was not found.");

            if (restaurant is null)
            {
                logger.LogError($"Restaurant with id: {request.Id} was not found.");
                throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id: {request.Id} was not found.");
            }

            return restaurant;
        }
    }
}
