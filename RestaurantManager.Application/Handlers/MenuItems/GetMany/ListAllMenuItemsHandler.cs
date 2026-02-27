using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Handlers.MenuItems.GetById;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.MenuItems.GetMany
{
    internal class ListAllMenuItemsHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<ListAllMenuItemsQuery, GetManyMenuItemsResponse>
    {
        public async Task<GetManyMenuItemsResponse> Handle(ListAllMenuItemsQuery request, CancellationToken cancellationToken)
        {
            var menuItems = await restaurantManagerDbContext.MenuItems
                .AsNoTracking()
                .ProjectTo<GetMenuItemByIdResponse>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new GetManyMenuItemsResponse(menuItems);
        }
    }
}
