using RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo;

namespace RestaurantManager.Application.Handlers.Restaurants.GetAllRestaurantInfos
{
    public record GetAllRestaurantInfosResponse(IEnumerable<GetRestaurantInfoResponse> Restaurants);
}
