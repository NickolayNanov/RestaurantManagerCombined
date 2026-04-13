using RestaurantManager.Application.Handlers.Auth;
namespace RestaurantManager.Application.Handlers.Restaurants.Update
{
    public class UpdateRestaurantCommandValidator : ApplicationValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.OwnerId)
                .NotNullNotEmptyRequired();

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
                .StringLengthBetween(3, 100);

            ValidatorsExtensions.IsInEnum(this.RuleFor(x => x.Status));

            this.RuleFor(x => x.ImgUrl)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);
        }
    }
}
