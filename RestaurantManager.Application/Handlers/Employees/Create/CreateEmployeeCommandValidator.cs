using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Application.Services;
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

            ValidatorsExtensions.IsInEnum(this.RuleFor(x => x.Status));

            this.RuleFor(x => x.Salary)
                .GreaterThanZero()
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Image)
                .NotNull()
                .WithMessage("The field Image is required.")
                .Must(ImageValidation.HasAllowedContentType)
                .WithMessage("Image must be a JPEG, PNG, or WEBP file.")
                .Must(x => x is not null && x.Length <= ImageValidation.MaxFileSize)
                .WithMessage("Image must be 5 MB or smaller.");
        }
    }
}
