namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Create
{
    public class CreateRestaurantMonthlyReportCommandValidator : ApplicationValidator<CreateRestaurantMonthlyReportCommand>
    {
        public CreateRestaurantMonthlyReportCommandValidator()
        {
            this.ValidateMonthlyReport();
        }
    }
}
