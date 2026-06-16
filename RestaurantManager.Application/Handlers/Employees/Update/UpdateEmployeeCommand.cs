using MediatR;
using RestaurantManager.Application.Handlers.Employees.Shared;
using RestaurantManager.Application.Services.Models;

namespace RestaurantManager.Application.Handlers.Employees.Update
{
    public record UpdateEmployeeCommand : EmployeeBase, IRequest<UpdateEmployeeResponse>
    {
        public Guid? Id { get; set; }

        public UploadFile Image { get; set; }
    }
}
