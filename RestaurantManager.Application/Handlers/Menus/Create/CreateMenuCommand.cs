using MediatR;
using RestaurantManager.Application.Handlers.Menus.Shared;

namespace RestaurantManager.Application.Handlers.Menus.Create
{
    public record CreateMenuCommand : MenuBase, IRequest<CreateMenuResponse>
    {
        public Guid? RestaurantId { get; set; }
    }
}
