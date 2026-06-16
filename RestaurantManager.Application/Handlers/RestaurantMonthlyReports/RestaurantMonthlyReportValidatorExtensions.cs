using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports
{
    internal static class RestaurantMonthlyReportValidatorExtensions
    {
        public static void ValidateMonthlyReport<T>(this ApplicationValidator<T> validator)
            where T : RestaurantMonthlyReportBase
        {
            validator.RuleFor(x => x.RestaurantId)
                .NotNullNotEmptyRequired();

            validator.RuleFor(x => x.Year)
                .InclusiveBetween(2000, 2100)
                .WithMessage("Year must be between 2000 and 2100.");

            validator.RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .WithMessage("Month must be between 1 and 12.");

            validator.RuleFor(x => x.Revenue)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Revenue must be greater than or equal to 0.");

            validator.RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5)
                .WithMessage("Rating must be between 0 and 5.");
        }
    }
}
