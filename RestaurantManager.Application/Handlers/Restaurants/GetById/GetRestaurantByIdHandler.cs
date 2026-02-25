using AutoMapper;
using MediatR;
using RestaurantManager.Application.Exceptions;
using RestaurantManager.Domain;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Handlers.Restaurants.GetById
{
    internal class GetRestaurantByIdHandler(
        IMapper mapper,
        IRestaurantManagerDbContext restaurantManagerDbContext) : IRequestHandler<GetRestaurantByIdQuery, GetRestaurantByIdResponse>
    {
        public async Task<GetRestaurantByIdResponse> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantManagerDbContext.Restaurants.FindAsync(request.Id)
                ?? throw new ResourceNotFoundException(nameof(Restaurant), $"Restaurant with id {request.Id} was not found.");

            var response = mapper.Map<GetRestaurantByIdResponse>(restaurant);

            return response;
        }
    }
}
