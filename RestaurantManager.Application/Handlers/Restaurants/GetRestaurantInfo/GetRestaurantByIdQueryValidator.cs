using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo
{
    public class GetRestaurantByIdQueryValidator : ApplicationValidator<GetRestaurantInfoQuery>
    {
        public GetRestaurantByIdQueryValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();
        }
    }
}
