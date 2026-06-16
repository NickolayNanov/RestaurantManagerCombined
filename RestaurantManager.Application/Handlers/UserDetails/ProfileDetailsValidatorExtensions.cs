using FluentValidation;
using RestaurantManager.Application.Handlers.Auth;
using System.Linq.Expressions;

namespace RestaurantManager.Application.Handlers.UserDetails
{
    internal static class ProfileDetailsValidatorExtensions
    {
        public static void OptionalProfileString<T>(this AbstractValidator<T> validator, Expression<Func<T, string>> expression)
        {
            var getValue = expression.Compile();

            validator.RuleFor(expression)
                .StringLengthBetween(3, 100)
                .When(x => !string.IsNullOrWhiteSpace(getValue(x)));
        }
    }
}
