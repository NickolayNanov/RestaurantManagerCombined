using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;

namespace RestaurantManager.Application.Handlers.Users.Create
{
    public class CreateUserCommandValidator : ApplicationValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            this.RuleFor(x => x.Username)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Email)
                .NotNullNotEmptyRequired()
                .EmailAddress()
                    .WithMessage("Email must be a valid email address.")
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Password)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.ConfirmPassword)
                .NotNullNotEmptyRequired()
                .Equal(x => x.Password)
                    .WithMessage("Passwords do not match.")
                .StringLengthBetween(3, 100);
        }
    }
}
