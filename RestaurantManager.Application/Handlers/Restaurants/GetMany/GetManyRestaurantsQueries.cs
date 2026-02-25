using MediatR;

namespace RestaurantManager.Application.Handlers.Restaurants.GetMany
{
    public record ListAllRestaurantsQuery() : IRequest<GetManyRestaurantsResponse>;
}
