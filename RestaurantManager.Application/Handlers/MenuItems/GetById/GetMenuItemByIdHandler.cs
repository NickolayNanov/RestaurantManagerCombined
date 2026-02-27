using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.MenuItems.GetById
{
    internal class GetMenuItemByIdHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<GetMenuItemByIdQuery, GetMenuItemByIdResponse>
    {
        public async Task<GetMenuItemByIdResponse> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
            var menuItem = await restaurantManagerDbContext.MenuItems
                .AsNoTracking()
                .ProjectTo<GetMenuItemByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(mi => mi.Id == request.Id, cancellationToken);

            return menuItem;
        }
    }
}
