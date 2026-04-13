using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Menus.GetById
{
    public class GetMenuByIdQueryValidator : ApplicationValidator<GetMenuByIdQuery>
    {
        public GetMenuByIdQueryValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();
        }
    }
}
