using FluentValidation;

namespace RestaurantManager.Application.Handlers.Restaurants.GetRestaurantInfo
{
    public class GetRestaurantByIdQueryValidator : ApplicationValidator<GetRestaurantInfoQuery>
    {
        public GetRestaurantByIdQueryValidator()
        {
            this.RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage);
        }
    }
}
