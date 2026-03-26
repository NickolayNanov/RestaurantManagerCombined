using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Categories.Delete
{
    public class DeleteCategoryValidator : ApplicationValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            this.RuleFor(x => x.Id)
            .NotNullNotEmptyRequired();
        }
    }
}
