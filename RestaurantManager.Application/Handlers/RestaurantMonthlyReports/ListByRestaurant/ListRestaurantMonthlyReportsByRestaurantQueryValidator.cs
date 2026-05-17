using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.ListByRestaurant
{
    public class ListRestaurantMonthlyReportsByRestaurantQueryValidator : ApplicationValidator<ListRestaurantMonthlyReportsByRestaurantQuery>
    {
        public ListRestaurantMonthlyReportsByRestaurantQueryValidator()
        {
            this.RuleFor(x => x.RestaurantId)
                .NotNullNotEmptyRequired();
        }
    }
}
