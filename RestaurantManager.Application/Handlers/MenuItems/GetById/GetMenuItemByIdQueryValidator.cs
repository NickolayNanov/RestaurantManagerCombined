using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.MenuItems.GetById
{
    public class GetMenuItemByIdQueryValidator : ApplicationValidator<GetMenuItemByIdQuery>
    {
        public GetMenuItemByIdQueryValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();
        }
    }
}
