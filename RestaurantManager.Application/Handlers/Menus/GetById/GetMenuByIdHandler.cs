using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Menus.GetById
{
    internal class GetMenuByIdHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<GetMenuByIdQuery, GetMenuByIdResponse>
    {
        public async Task<GetMenuByIdResponse> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await restaurantManagerDbContext.Menus
                .AsNoTracking()
                .Where(m => m.Id == request.Id)
                .ProjectTo<GetMenuByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException(nameof(Menu), $"Menu with id {request.Id} was not found.");

            return menu;
        }
    }
}
