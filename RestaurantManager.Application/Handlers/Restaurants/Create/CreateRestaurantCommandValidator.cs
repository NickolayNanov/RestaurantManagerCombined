using FluentValidation;

namespace RestaurantManager.Application.Handlers.Restaurants.Create
{
    public class CreateRestaurantCommandValidator : ApplicationValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(100)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Name), 3, 100))
                .MinimumLength(3)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Name), 3, 100));

            this.RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(200)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Description), 1, 200))
                .MinimumLength(1)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Description), 1, 200));

            this.RuleFor(x => x.ImgUrl)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage);
        }
    }
}
