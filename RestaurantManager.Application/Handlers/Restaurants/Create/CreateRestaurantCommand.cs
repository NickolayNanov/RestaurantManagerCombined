using MediatR;

namespace RestaurantManager.Application.Handlers.Restaurants.Create
{
    public record CreateRestaurantCommand(string Name, string Description, string ImgUrl) : IRequest<CreateRestaurantResponse>;
}
