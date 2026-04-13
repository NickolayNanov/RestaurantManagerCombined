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
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Position)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.PhoneNumber)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            ValidatorsExtensions.IsInEnum(this.RuleFor(x => x.EmploymentType));

            this.RuleFor(x => x.Salary)
                .GreaterThanZero()
                .NotNullNotEmptyRequired();
        }
    }
}
