using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Application.Handlers.UserDetails;

namespace RestaurantManager.Application.Handlers.UserDetails.Create
{
    public class CreateProfileDetailsCommandValidator : ApplicationValidator<CreateProfileDetailsCommand>
    {
        public CreateProfileDetailsCommandValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotNullNotEmptyRequired();

            this.OptionalProfileString(x => x.FirstName);
            this.OptionalProfileString(x => x.Surname);
            this.OptionalProfileString(x => x.LastName);
            this.OptionalProfileString(x => x.CompanyName);
            this.OptionalProfileString(x => x.PhoneNumber);
        }
    }
}
