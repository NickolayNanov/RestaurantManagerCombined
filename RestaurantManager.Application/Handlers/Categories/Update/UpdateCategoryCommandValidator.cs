using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Categories.Update
{
    public class UpdateCategoryCommandValidator : ApplicationValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);
        }
    }
}
