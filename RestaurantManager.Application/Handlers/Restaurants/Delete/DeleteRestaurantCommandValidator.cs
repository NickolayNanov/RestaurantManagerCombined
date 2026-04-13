using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Restaurants.Delete
{
    public class DeleteRestaurantCommandValidator : ApplicationValidator<DeleteRestaurantCommand>
    {
        public DeleteRestaurantCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();
        }
    }
}
