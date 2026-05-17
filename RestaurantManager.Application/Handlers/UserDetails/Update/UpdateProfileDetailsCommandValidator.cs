using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Application.Handlers.UserDetails;
using RestaurantManager.Application.Services;

namespace RestaurantManager.Application.Handlers.UserDetails.Update
{
    public class UpdateProfileDetailsCommandValidator : ApplicationValidator<UpdateProfileDetailsCommand>
    {
        public UpdateProfileDetailsCommandValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotNullNotEmptyRequired();

            this.OptionalProfileString(x => x.FirstName);
            this.OptionalProfileString(x => x.Surname);
            this.OptionalProfileString(x => x.LastName);
            this.OptionalProfileString(x => x.CompanyName);
            this.OptionalProfileString(x => x.PhoneNumber);

            this.RuleFor(x => x.Image)
                .Must(x => x is null || ImageValidation.HasAllowedContentType(x))
                .WithMessage("Image must be a JPEG, PNG, or WEBP file.")
                .Must(x => x is null || x.Length <= ImageValidation.MaxFileSize)
                .WithMessage("Image must be 5 MB or smaller.");
        }
    }
}
