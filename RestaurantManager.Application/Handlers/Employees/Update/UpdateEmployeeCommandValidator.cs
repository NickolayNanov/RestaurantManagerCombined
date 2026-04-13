using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Employees.Update
{
    public class UpdateEmployeeCommandValidator : ApplicationValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            this.RuleFor(x => x.Id)
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
