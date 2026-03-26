using FluentValidation;

namespace RestaurantManager.Application.Handlers.Auth
{
    public class AuthQueryValidator : ApplicationValidator<AuthQuery>
    {
        public AuthQueryValidator()
        {
            this.RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotNullNotEmptyRequired();

                this.RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotNullNotEmptyRequired();
        }
    }
}
