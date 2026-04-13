using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Menus.Delete
{
    public class DeleteMenuCommandValidator : ApplicationValidator<DeleteMenuCommand>
    {
        public DeleteMenuCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();
        }
    }
}
