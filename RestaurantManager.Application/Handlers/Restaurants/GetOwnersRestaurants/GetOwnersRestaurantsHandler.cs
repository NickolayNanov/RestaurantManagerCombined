using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Handlers.Restaurants.GetById;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.GetOwnersRestaurants
{
    internal class GetOwnersRestaurantsHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<GetOwnersRestaurantsQuery, GetOwnersRestaurantsResponse>
    {
        public async Task<GetOwnersRestaurantsResponse> Handle(GetOwnersRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await restaurantManagerDbContext.Restaurants
                .AsSplitQuery()
                .AsNoTracking()
                .ProjectTo<GetRestaurantByIdResponse>(mapper.ConfigurationProvider)
                .Where(r => r.OwnerId == currentUserService.UserId)
                .ToListAsync(cancellationToken);

            return new GetOwnersRestaurantsResponse(restaurants);
        }
    }
}
