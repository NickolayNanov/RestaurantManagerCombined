using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Menus.Update
{
    public class UpdateMenuCommandValidator : ApplicationValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Description)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.ImgUrl)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Type)
                .IsInEnum();

            this.RuleFor(x => x.RestaurantId)
                .NotNullNotEmptyRequired();
        }
    }
}
