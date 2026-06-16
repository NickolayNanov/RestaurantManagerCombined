namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Update
{
    public class UpdateRestaurantMonthlyReportCommandValidator : ApplicationValidator<UpdateRestaurantMonthlyReportCommand>
    {
        public UpdateRestaurantMonthlyReportCommandValidator()
        {
            this.ValidateMonthlyReport();
        }
    }
}
