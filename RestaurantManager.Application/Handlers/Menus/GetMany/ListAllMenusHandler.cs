using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Handlers.Menus.GetById;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Menus.GetMany
{
    internal class ListAllMenusHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<ListAllMenusQuery, GetManyMenusResponse>
    {
        public async Task<GetManyMenusResponse> Handle(ListAllMenusQuery request, CancellationToken cancellationToken)
        {
            var menu = await restaurantManagerDbContext.Menus
                .AsNoTracking()
                .ProjectTo<GetMenuByIdResponse>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetManyMenusResponse(menu);
        }
    }
}
