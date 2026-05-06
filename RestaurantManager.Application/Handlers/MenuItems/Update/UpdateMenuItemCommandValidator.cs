using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using RestaurantManager.Application.Services;

namespace RestaurantManager.Application.Handlers.MenuItems.Update
{
    public class UpdateMenuItemCommandValidator : ApplicationValidator<UpdateMenuItemCommand>
    {
        public UpdateMenuItemCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.Name)
                .NotNullNotEmptyRequired()
                .StringLengthBetween(3, 100);

            this.RuleFor(x => x.Price)
                .GreaterThanZero()
                .NotNullNotEmptyRequired();

            this.RuleFor(x => x.CategoryId)
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
