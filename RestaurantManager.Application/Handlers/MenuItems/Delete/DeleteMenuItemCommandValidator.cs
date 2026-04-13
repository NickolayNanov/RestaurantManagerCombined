using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.MenuItems.Delete
{
    public class DeleteMenuItemCommandValidator : ApplicationValidator<DeleteMenuItemCommand>
    {
        public DeleteMenuItemCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();
        }
    }
}
