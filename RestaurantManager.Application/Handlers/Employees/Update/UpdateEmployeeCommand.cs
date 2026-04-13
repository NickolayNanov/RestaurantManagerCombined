using MediatR;
using RestaurantManager.Application.Handlers.Employees.Shared;

namespace RestaurantManager.Application.Handlers.Employees.Update
{
    public record UpdateEmployeeCommand : EmployeeBase, IRequest<UpdateEmployeeResponse>
    {
        public Guid? Id { get; set; }
    }
}
