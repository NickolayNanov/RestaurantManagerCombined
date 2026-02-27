using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo
{
    internal class GetRestaurantInfoHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<GetRestaurantInfoQuery, GetRestaurantInfoResponse>
    {
        public async Task<GetRestaurantInfoResponse> Handle(GetRestaurantInfoQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantManagerDbContext
                .Restaurants
                .AsNoTracking()
                .ProjectTo<GetRestaurantInfoResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.Id == request.Id)
                ?? throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id {request.Id} was not found.");

            var response = mapper.Map<GetRestaurantInfoResponse>(restaurant);

            return response;
        }
    }
}
