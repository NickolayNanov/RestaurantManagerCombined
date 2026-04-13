using MediatR;
using RestaurantManager.Application.Handlers.Categories.Shared;

namespace RestaurantManager.Application.Handlers.Categories.Update
{
    public record UpdateCategoryCommand : CategoryBase, IRequest<UpdateCategoryResponse>
    {
        public Guid Id { get; set; }
    }
}
