namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports
{
    public record RestaurantMonthlyReportResponse(
        Guid Id,
        Guid RestaurantId,
        int Year,
        int Month,
        decimal Revenue,
        decimal Rating,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}
