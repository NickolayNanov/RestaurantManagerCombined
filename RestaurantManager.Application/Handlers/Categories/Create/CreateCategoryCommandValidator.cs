using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Categories.Create
{
    public class CreateCategoryCommandValidator : ApplicationValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);
        }
    }
}
