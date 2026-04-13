using AutoMapper;
using RestaurantManager.Application.Handlers.Employees.Create;
using RestaurantManager.Application.Handlers.Employees.GetById;
using RestaurantManager.Application.Handlers.Employees.Update;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Mappings
{
    public class EmployeesApplicationProfile : Profile
    {
        public EmployeesApplicationProfile()
        {
            // get single
            this.CreateMap<Employee, GetEmployeeByIdResponse>();

            // create
            this.CreateMap<CreateEmployeeCommand, Employee>();
            this.CreateMap<Employee, CreateEmployeeResponse>();

            // update
            this.CreateMap<UpdateEmployeeCommand, Employee>();
            this.CreateMap<Employee, UpdateEmployeeResponse>();
        }
    }
}
