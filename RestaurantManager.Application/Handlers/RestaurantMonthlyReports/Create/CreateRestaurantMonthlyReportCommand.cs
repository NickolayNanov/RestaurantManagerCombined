using MediatR;

namespace RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Create
{
    public record CreateRestaurantMonthlyReportCommand : RestaurantMonthlyReportBase, IRequest<RestaurantMonthlyReportResponse>;
}
