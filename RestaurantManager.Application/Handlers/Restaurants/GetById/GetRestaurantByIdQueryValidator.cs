using FluentValidation;

namespace RestaurantManager.Application.Handlers.Restaurants.GetById
{
    public class GetRestaurantByIdQueryValidator : ApplicationValidator<GetRestaurantByIdQuery>
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
