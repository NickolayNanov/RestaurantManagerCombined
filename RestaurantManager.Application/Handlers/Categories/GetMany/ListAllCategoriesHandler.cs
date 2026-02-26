using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Handlers.Categories.GetById;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Categories.GetMany
{
    internal class ListAllCategoriesHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<ListAllCategoriesQuery, GetManyCategoriesResponse>
    {
        public async Task<GetManyCategoriesResponse> Handle(ListAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var menus = await mapper.ProjectTo<GetCategoryByIdResponse>(restaurantManagerDbContext.Categories.AsNoTracking()).ToListAsync() ?? [];
            return new GetManyCategoriesResponse(menus);
        }
    }
}
