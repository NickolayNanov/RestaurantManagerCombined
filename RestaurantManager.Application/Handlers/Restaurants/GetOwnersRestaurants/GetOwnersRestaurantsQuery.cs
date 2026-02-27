using MediatR;

namespace RestaurantManager.Application.Handlers.Restaurants.GetOwnersRestaurants
{
    public class GetOwnersRestaurantsQuery : IRequest<GetOwnersRestaurantsResponse>;
}
