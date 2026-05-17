using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.GetByMonth
{
    public class GetRestaurantMonthlyReportByMonthQueryValidator : ApplicationValidator<GetRestaurantMonthlyReportByMonthQuery>
    {
        public GetRestaurantMonthlyReportByMonthQueryValidator()
        {
            this.RuleFor(x => x.RestaurantId)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Year)
                .InclusiveBetween(2000, 2100)
                .WithMessage("Year must be between 2000 and 2100.");

            this.RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .WithMessage("Month must be between 1 and 12.");
        }
    }
}
