using MediatR;

namespace RestaurantManager.Application.Handlers.MenuItems.Delete
{
    public record DeleteMenuItemCommand(Guid Id) : IRequest<DeleteMenuItemResponse>;
}
