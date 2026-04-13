using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.MenuItems.Update
{
    public class UpdateMenuItemCommandValidator : ApplicationValidator<UpdateMenuItemCommand>
    {
        public UpdateMenuItemCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Price)
                .GreaterThanZero()
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.ImgUrl)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.CategoryId)
                .NotNullNotEmptyRequired();
        }
    }
}
