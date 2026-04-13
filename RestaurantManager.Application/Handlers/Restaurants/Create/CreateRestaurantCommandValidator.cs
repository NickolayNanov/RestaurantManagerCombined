using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.Create
{
    public class CreateRestaurantCommandValidator : ApplicationValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Description)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Location)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Cuisine)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(1, 100);

            this.RuleFor(x => x.ImgUrl)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            ValidatorsExtensions.IsInEnum(this.RuleFor(x => x.Status));
        }
    }
}
