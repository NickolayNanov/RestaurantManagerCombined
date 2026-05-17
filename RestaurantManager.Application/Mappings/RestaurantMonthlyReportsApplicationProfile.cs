using AutoMapper;
using RestaurantManager.Application.Handlers.RestaurantMonthlyReports;
using RestaurantManager.Application.Handlers.RestaurantMonthlyReports.Create;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Mappings
{
    public class RestaurantMonthlyReportsApplicationProfile : Profile
    {
        public RestaurantMonthlyReportsApplicationProfile()
        {
            this.CreateMap<CreateRestaurantMonthlyReportCommand, RestaurantMonthlyReport>();
            this.CreateMap<RestaurantMonthlyReport, RestaurantMonthlyReportResponse>();
        }
    }
}
