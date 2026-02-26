using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.MenuItems.GetById
{
    internal class GetMenuItemByIdHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<GetMenuItemByIdQuery, GetMenuItemByIdResponse>
    {
        public async Task<GetMenuItemByIdResponse> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await restaurantManagerDbContext
                .MenuItems
                .Include(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == request.Id)
                ?? throw new ResourceNotFoundException(nameof(MenuItem), $"Menu Item with id {request.Id} was not found.");

            var response = mapper.Map<GetMenuItemByIdResponse>(menu);
            response.CategoryText = menu.Category.Name;

            return response;
        }
    }
}
