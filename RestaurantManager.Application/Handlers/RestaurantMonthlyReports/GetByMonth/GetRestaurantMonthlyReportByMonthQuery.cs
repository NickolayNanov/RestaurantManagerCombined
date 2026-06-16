using MediatR;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.GetByMonth
{
    public record GetRestaurantMonthlyReportByMonthQuery(Guid RestaurantId, int Year, int Month) : IRequest<RestaurantMonthlyReportResponse>;
}
