using MediatR;

namespace RestaurantManager.Application.Handlers.Restaurants.GetAllRestaurantInfos
{
    public record GetAllRestaurantInfosQuery() : IRequest<GetAllRestaurantInfosResponse>;
}
