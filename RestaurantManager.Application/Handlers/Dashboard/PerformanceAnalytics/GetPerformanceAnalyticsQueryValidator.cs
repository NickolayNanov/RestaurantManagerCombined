using FluentValidation;

namespace RestaurantManager.Application.Handlers.Dashboard.PerformanceAnalytics
{
    public class GetPerformanceAnalyticsQueryValidator : ApplicationValidator<GetPerformanceAnalyticsQuery>
    {
        public GetPerformanceAnalyticsQueryValidator()
        {
            this.RuleFor(x => x.Months)
                .InclusiveBetween(1, 24)
                .WithMessage("Months must be between 1 and 24.");
        }
    }
}
