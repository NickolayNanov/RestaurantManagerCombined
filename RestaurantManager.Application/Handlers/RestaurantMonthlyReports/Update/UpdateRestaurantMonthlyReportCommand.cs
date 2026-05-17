using MediatR;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Update
{
    public record UpdateRestaurantMonthlyReportCommand : RestaurantMonthlyReportBase, IRequest<RestaurantMonthlyReportResponse>;
}
