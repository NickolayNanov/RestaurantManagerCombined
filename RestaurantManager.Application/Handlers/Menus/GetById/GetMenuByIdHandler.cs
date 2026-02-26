using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Menus.GetById
{
    internal class GetMenuByIdHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext
        ) : IRequestHandler<GetMenuByIdQuery, GetMenuByIdResponse>
    {
        public async Task<GetMenuByIdResponse> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await restaurantManagerDbContext.Menus.AsNoTracking().FirstOrDefaultAsync(r => r.Id == request.Id)
                ?? throw new ResourceNotFoundException(nameof(Menu), $"Menu with id {request.Id} was not found.");

            var response = mapper.Map<GetMenuByIdResponse>(menu);

            return response;
        }
    }
}
