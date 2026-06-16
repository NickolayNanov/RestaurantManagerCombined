using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Application.Services;

namespace RestaurantManager.Application.Handlers.Menus.Update
{
    public class UpdateMenuCommandValidator : ApplicationValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Description)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            ValidatorsExtensions.IsInEnum(this.RuleFor(x => x.Type));

            this.RuleFor(x => x.RestaurantId)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Image)
                .Must(ImageValidation.HasAllowedContentType)
                .WithMessage("Image must be a JPEG, PNG, or WEBP file.")
                .When(x => x.Image is not null);

            this.RuleFor(x => x.Image.Length)
                .LessThanOrEqualTo(ImageValidation.MaxFileSize)
                .WithMessage("Image must be 5 MB or smaller.")
                .When(x => x.Image is not null);
        }
    }
}
