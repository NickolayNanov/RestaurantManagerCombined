using FluentValidation;

namespace RestaurantManager.Application
{
    public abstract class ApplicationValidator<T> : AbstractValidator<T> 
    {
        public static string NullOrEmptyMessage => "The property {PropertyName} is null or empty.";

        public static string StringLengthErrorMessage => "The property {0} must be between {1} and {2} characters in length.";

        public static string InvalidEnumValueErrorMessage => "The property {0} must contain valid enum values: [{1}].";
    }
}
