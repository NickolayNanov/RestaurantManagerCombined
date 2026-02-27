using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo;
using RestaurantManager.Application.Services.Interfaces;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.GetAllRestaurantInfos
{
    internal class GetAllRestaurantInfosHandler(
        IMapper mapper,
        ICurrentUserService currentUserService,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<GetAllRestaurantInfosQuery, GetAllRestaurantInfosResponse>
    {
        public async Task<GetAllRestaurantInfosResponse> Handle(GetAllRestaurantInfosQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await restaurantManagerDbContext.Restaurants
                .AsSplitQuery()
                .AsNoTracking()
                .ProjectTo<GetRestaurantInfoResponse>(mapper.ConfigurationProvider)
                .Where(r => r.OwnerId == currentUserService.UserId)
                .ToListAsync();

            return new GetAllRestaurantInfosResponse(restaurants);
        }
    }
}
