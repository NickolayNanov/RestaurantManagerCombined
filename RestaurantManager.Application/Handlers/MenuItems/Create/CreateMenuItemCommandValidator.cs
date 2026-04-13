using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.MenuItems.Create
{
    public class CreateMenuItemCommandValidator : ApplicationValidator<CreateMenuItemCommand>
    {
        public CreateMenuItemCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Price)
                .GreaterThanZero()
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.ImgUrl)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.MenuId)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.CategoryId)
                .NotNullNotEmptyRequired();
        }
    }
}
