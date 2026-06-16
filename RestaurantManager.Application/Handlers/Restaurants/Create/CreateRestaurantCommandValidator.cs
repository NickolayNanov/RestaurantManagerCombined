using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Application.Services;
using RestaurantManager.Domain;

namespace RestaurantManager.Application.Handlers.Restaurants.Create
{
    public class CreateRestaurantCommandValidator : ApplicationValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Description)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Location)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Cuisine)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(1, 100);

            ValidatorsExtensions.IsInEnum(this.RuleFor(x => x.Status));

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
