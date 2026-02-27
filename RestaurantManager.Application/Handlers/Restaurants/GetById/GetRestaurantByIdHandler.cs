using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Restaurants.GetById
{
    internal class GetRestaurantByIdHandler(
        ILogger<GetRestaurantByIdHandler> logger,
        IMapper mapper,
        IRestaurantManagerDbContext dbContext) : IRequestHandler<GetRestaurantByIdQuery, GetRestaurantByIdResponse>
    {
        public async Task<GetRestaurantByIdResponse> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await dbContext.Restaurants
                .AsSplitQuery()
                .AsNoTracking()
                .ProjectTo<GetRestaurantByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (restaurant is null)
            {
                logger.LogError($"Restaurant with id: {request.Id} was not found.");
                throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id: {request.Id} was not found.");
            }

            return restaurant;
        }
    }
}
