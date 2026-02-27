using RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo;

namespace RestaurantManager.Application.Handlers.Restaurants.GetMany
{
    public record GetManyRestaurantsResponse(IEnumerable<GetRestaurantInfoResponse> Restaurants);
}
