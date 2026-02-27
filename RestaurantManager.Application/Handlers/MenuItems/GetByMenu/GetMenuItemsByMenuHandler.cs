using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Handlers.MenuItems.GetById;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.MenuItems.GetByMenu
{
    internal class GetMenuItemsByMenuHandler(
        IMapper mapper,
        IRestaurantManagerDbContext dbContext
        ) : IRequestHandler<GetMenuItemsByMenuQuery, GetMenuItemsByMenuResponse>
    {
        public async Task<GetMenuItemsByMenuResponse> Handle(GetMenuItemsByMenuQuery request, CancellationToken cancellationToken)
        {
            var menuItems = await dbContext.MenuItems
                .AsSplitQuery()
                .AsNoTracking()
                .ProjectTo<GetMenuItemByIdResponse>(mapper.ConfigurationProvider)
                .Where(mi => mi.MenuId == request.MenuId)
                .ToListAsync(cancellationToken);

            return new GetMenuItemsByMenuResponse(menuItems);
        }
    }
}
