using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Employees.Delete
{
    public class DeleteEmployeeCommandValidator : ApplicationValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();
        }
    }
}
