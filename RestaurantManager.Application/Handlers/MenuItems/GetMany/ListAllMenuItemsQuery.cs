using MediatR;

namespace RestaurantManager.Application.Handlers.MenuItems.GetMany
{
    public record ListAllMenuItemsQuery : IRequest<GetManyMenuItemsResponse>;
}
