using RestaurantManager.Application.Handlers.Restaurants.GetById;

namespace RestaurantManager.Application.Handlers.Restaurants.GetMany
{
    public record GetManyRestaurantsResponse(IEnumerable<GetRestaurantByIdResponse> Restaurants);
}
