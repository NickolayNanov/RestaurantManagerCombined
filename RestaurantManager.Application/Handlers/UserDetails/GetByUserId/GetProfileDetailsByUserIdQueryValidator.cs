using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.UserDetails.GetByUserId
{
    public class GetProfileDetailsByUserIdQueryValidator : ApplicationValidator<GetProfileDetailsByUserIdQuery>
    {
        public GetProfileDetailsByUserIdQueryValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotNullNotEmptyRequired();
        }
    }
}
