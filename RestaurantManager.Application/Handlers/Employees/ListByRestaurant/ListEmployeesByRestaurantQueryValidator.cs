using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Employees.ListByRestaurant
{
    public class ListEmployeesByRestaurantQueryValidator : ApplicationValidator<ListEmployeesByRestaurantQuery>
    {
        public ListEmployeesByRestaurantQueryValidator()
        {
            this.RuleFor(x => x.RestaurantId)
                .NotNullNotEmptyRequired();
        }
    }
}
