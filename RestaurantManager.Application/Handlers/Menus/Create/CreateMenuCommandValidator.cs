using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Application.Services;

namespace RestaurantManager.Application.Handlers.Menus.Create
{
    public class CreateMenuCommandValidator : ApplicationValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
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
                .NotNull()
                .WithMessage("Image is required.")
                .Must(ImageValidation.HasAllowedContentType)
                .WithMessage("Image must be a JPEG, PNG, or WEBP file.");

            this.RuleFor(x => x.Image.Length)
                .LessThanOrEqualTo(ImageValidation.MaxFileSize)
                .WithMessage("Image must be 5 MB or smaller.")
                .When(x => x.Image is not null);
        }
    }
}
