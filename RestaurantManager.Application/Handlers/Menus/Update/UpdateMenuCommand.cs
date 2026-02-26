using MediatR;
using RestaurantManager.Application.Handlers.Menus.Shared;

namespace RestaurantManager.Application.Handlers.Menus.Update
{
    public record UpdateMenuCommand : MenuBase, IRequest<UpdateMenuResponse>
    {
        public Guid? Id { get; set; }
    }
}
