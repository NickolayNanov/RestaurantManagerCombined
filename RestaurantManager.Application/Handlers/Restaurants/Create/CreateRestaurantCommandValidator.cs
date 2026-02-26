using FluentValidation;
using RestaurantManager.Domain;

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

            this.RuleFor(x => x.Location)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(200)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Location), 1, 200))
                .MinimumLength(1)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Location), 1, 200));

            this.RuleFor(x => x.Cuisine)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(100)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Cuisine), 1, 100))
                .MinimumLength(1)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Cuisine), 1, 100));

            this.RuleFor(x => x.ImgUrl)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage);

            this.RuleFor(x => x.Status)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .IsInEnum()
                    .WithMessage(string.Format(InvalidEnumValueErrorMessage, nameof(CreateRestaurantCommand.Status), string.Join(", ", Enum.GetNames<OpenClosed>())));
        }
    }
}
