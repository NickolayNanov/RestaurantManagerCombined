using MediatR;

namespace RestaurantManager.Application.Handlers.Menus.GetMany
{
    public record ListAllMenusQuery() : IRequest<GetManyMenusResponse>;
}
