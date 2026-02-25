using MediatR;

namespace RestaurantManager.Application.Handlers.Restaurants.GetById
{
    public record GetRestaurantByIdQuery(Guid? Id) : IRequest<GetRestaurantByIdResponse>;
}
