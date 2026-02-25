using FluentValidation;
using RestaurantManager.Application.Handlers.Restaurants.Create;

namespace RestaurantManager.Application.Handlers.Restaurants.Update
{
    public class UpdateRestaurantCommandValidator : ApplicationValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            this.RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage);

            this.RuleFor(x => x.OwnerId)
                .Cascade(CascadeMode.Stop)
                .Must(x => x != Guid.Empty)
                    .When(x => x is not null)
                    .WithMessage(NullOrEmptyMessage);

            this.RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(50)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Name), 3, 50))
                .MinimumLength(3)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Name), 3, 50));

            this.RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage)
                .MaximumLength(200)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Description), 1, 200))
                .MinimumLength(1)
                    .WithMessage(string.Format(StringLengthErrorMessage, nameof(CreateRestaurantCommand.Description), 1, 200));

            this.RuleFor(x => x.ImgUrl)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage(NullOrEmptyMessage)
                .NotEmpty()
                    .WithMessage(NullOrEmptyMessage);
        }
    }
}
