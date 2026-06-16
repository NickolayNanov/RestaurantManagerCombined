namespace RestaurantManager.Application.Handlers.Dashboard.PerformanceAnalytics
{
    public record GetPerformanceAnalyticsResponse(IEnumerable<PerformanceAnalyticsMonthResponse> Months);

    public record PerformanceAnalyticsMonthResponse(
        int Year,
        int Month,
        string Label,
        decimal Revenue,
        decimal? Rating);
}
