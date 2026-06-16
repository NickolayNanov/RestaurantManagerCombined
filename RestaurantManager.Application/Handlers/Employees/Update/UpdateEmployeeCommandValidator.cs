using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Application.Services;

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

            ValidatorsExtensions.IsInEnum(this.RuleFor(x => x.Status));

            this.RuleFor(x => x.Salary)
                .GreaterThanZero()
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Image)
                .Must(x => x is null || ImageValidation.HasAllowedContentType(x))
                .WithMessage("Image must be a JPEG, PNG, or WEBP file.")
                .Must(x => x is null || x.Length <= ImageValidation.MaxFileSize)
                .WithMessage("Image must be 5 MB or smaller.");
        }
    }
}
