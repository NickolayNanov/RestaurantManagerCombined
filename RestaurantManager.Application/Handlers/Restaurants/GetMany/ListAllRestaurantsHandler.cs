using AutoMapper;
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
            var items = await mapper.ProjectTo<GetRestaurantByIdResponse>(restaurantManagerDbContext.Restaurants).ToListAsync() ?? [];
            return new GetManyRestaurantsResponse(items);
        }
    }
}
