namespace RestaurantManager.Application.Handlers.Auth
{
    public class AuthQueryValidator : ApplicationValidator<AuthQuery>
    {
        public AuthQueryValidator()
        {
            this.RuleFor(x => x.Email)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Password)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);
        }
    }
}
