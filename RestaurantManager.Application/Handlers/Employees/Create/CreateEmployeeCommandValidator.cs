using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Employees.Create
{
    public class CreateEmployeeCommandValidator : ApplicationValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            this.RuleFor(x => x.RestaurantId)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Email)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Position)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.PhoneNumber)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.EmploymentType)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Salary)
                .GreaterThanZero()
                .NotNullNotEmptyRequired();
        }
    }
}
