using MediatR;

namespace RestaurantManager.Application.Handlers.Menus.Delete
{
    public record DeleteMenuCommand(Guid Id) : IRequest<DeleteMenuResponse>;
}
