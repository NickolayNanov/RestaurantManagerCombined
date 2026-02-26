using AutoMapper;
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
            var menus = await mapper.ProjectTo<GetMenuByIdResponse>(restaurantManagerDbContext.Menus.AsNoTracking()).ToListAsync() ?? [];
            return new GetManyMenusResponse(menus);
        }
    }
}
