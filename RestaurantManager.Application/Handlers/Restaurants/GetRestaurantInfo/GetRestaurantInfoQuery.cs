using MediatR;

namespace RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo
{
    public record GetRestaurantInfoQuery(Guid? Id) : IRequest<GetRestaurantInfoResponse>;
}
