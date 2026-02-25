using FluentValidation;

namespace RestaurantManager.Application.Handlers.Users.Create
{
    public class CreateUserCommandValidator : ApplicationValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(100)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateUserCommand.Username), 3, 100))
                .MinimumLength(3)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateUserCommand.Username), 3, 100));

            this.RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(100)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateUserCommand.Email), 3, 100))
                .MinimumLength(3)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateUserCommand.Email), 3, 100));

            this.RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(100)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateUserCommand.Password), 3, 100))
                .MinimumLength(3)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateUserCommand.Password), 3, 100));

            this.RuleFor(x => x.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .Equal(x => x.Password)
                    .WithMessage("Passwords do not match.")
                .MaximumLength(100)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateUserCommand.ConfirmPassword), 3, 100))
                .MinimumLength(3)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateUserCommand.ConfirmPassword), 3, 100));
        }
    }
}
