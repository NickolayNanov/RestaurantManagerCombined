namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.ListByRestaurant
{
    public record ListRestaurantMonthlyReportsByRestaurantResponse(IEnumerable<RestaurantMonthlyReportResponse> Reports);
}
