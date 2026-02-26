using AutoMapper;
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
            var items = await restaurantManagerDbContext.MenuItems.Include(mi => mi.Category).AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
            var menuItems = items.Select(mapper.Map<GetMenuItemByIdResponse>).ToList();

            menuItems.ForEach(mi => mi.CategoryText = items.First(i => i.Id == mi.Id).Category.Name);

            return new GetManyMenuItemsResponse(menuItems);
        }
    }
}
