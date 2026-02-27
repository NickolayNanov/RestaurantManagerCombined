using RestaurantManager.Application.Handlers.Restaurants.GetById;

namespace RestaurantManager.Application.Handlers.Restaurants.GetOwnersRestaurants
{
    public record GetOwnersRestaurantsResponse(IEnumerable<GetRestaurantByIdResponse> Restaurants);
}
