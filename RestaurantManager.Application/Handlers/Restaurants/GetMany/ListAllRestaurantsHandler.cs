using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Handlers.Restaurants.GetById;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.GetMany
{
    internal class ListAllRestaurantsHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<ListAllRestaurantsQuery, GetManyRestaurantsResponse>
    {
        public async Task<GetManyRestaurantsResponse> Handle(ListAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await restaurantManagerDbContext.Restaurants
                .AsNoTracking()
                .ProjectTo<GetRestaurantByIdResponse>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new GetManyRestaurantsResponse(restaurants);
        }
    }
}
