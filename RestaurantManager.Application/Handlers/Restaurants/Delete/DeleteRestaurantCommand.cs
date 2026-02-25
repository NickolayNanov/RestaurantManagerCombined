using MediatR;

namespace RestaurantManager.Application.Handlers.Restaurants.Delete
{
    public record DeleteRestaurantCommand(Guid? Id) : IRequest<DeleteRestaurantResponse>;
}
