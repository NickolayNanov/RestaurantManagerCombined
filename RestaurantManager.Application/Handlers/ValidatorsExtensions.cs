using System.Collections.Concurrent;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;

namespace RestaurantManager.Application.Handlers.Auth
{
    public static class ValidatorsExtensions
    {
        private static readonly ConcurrentDictionary<Type, (string Values, string Names)> EnumValueAndNameCache = new();

        public const string ProvidedTypeNotEnumMessage = "The provided type was not of type Enum.";
        public const string InvalidEnumMessage = "The enumeration: {0} must be a value between [{1}] or in the following list: [{2}].";
        public const string InvalidMaxLengthMessage = "The length of {0} must be  between {1} and {2} characters.";
        public const string InvalidEmptyInputArray = "The array of {0} cannot be empty.";
        public const string InvalidNullInputArray = "The array of {0} cannot be null.";
        public const string RequiredErrorMessage = "The field {0} is required.";
        public const string MinimumLengthErrorMessage = "The length of {0} must be at least {1} characters.";
        public const string MaximumLengthErrorMessage = "The length of {0} must be at most {1} characters.";
        public const string NotEmptyMessage = "The field {0} is can't be empty.";
        public const string GreaterThanErrorMessage = "The field {0} must have value greater than {1}.";
        public const string GreaterThanOrEqualToErrorMessage = "The property {0} must have value greater than or equal to {1}.";
        public const string Property = "property";
        public const string NULL = "NULL";
        public const string PropertyNameVariableQuotes = "{PropertyName}";
        public static string InvalidEnumValueErrorMessage => "The property {0} must contain valid enum values: [{1}].";

        public static IRuleBuilderOptions<T, TProperty> NotNullNotEmptyRequired<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            var builder =
                ruleBuilder
                .SetValidator(new NotEmptyValidator<T, TProperty>())
                .WithMessage(string.Format(RequiredErrorMessage, "{PropertyName}"));

            if (Nullable.GetUnderlyingType(typeof(TProperty)) == typeof(Guid))
            {
                builder = builder.Must(value =>
                {
                    var guidValue = (Guid?)(object)value;
                    return !guidValue.HasValue || guidValue.Value != Guid.Empty;
                })
                .WithMessage(string.Format(RequiredErrorMessage, "{PropertyName}"));
            }

            return builder;
        }

        public static IRuleBuilderOptions<T, string> MinimumStringLength<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength)
        {
            return ruleBuilder.MinimumLength(minimumLength)
                .WithMessage(string.Format(MinimumLengthErrorMessage, PropertyNameVariableQuotes, minimumLength));
        }

        public static IRuleBuilderOptions<T, string> MaximumStringLength<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
        {
            return ruleBuilder.MaximumLength(maximumLength)
                .WithMessage(string.Format(MaximumLengthErrorMessage, PropertyNameVariableQuotes, maximumLength));
        }

        public static IRuleBuilderOptions<T, string> StringLengthBetween<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength, int maximumLength)
        {
            return ruleBuilder
                .MinimumStringLength(minimumLength)
                .MaximumStringLength(maximumLength);
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThanZero<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
            where TProperty : IComparable<TProperty>, IComparable, INumber<TProperty>
        {
            return ruleBuilder.SetValidator(new GreaterThanValidator<T, TProperty>(TProperty.Zero))
                .WithMessage((t, tProp) => GetValueGreaterThanErrorMessage(property: tProp, mustBeGreaterThan: TProperty.Zero, manualPropertyName: PropertyNameVariableQuotes));
        }

        public static IRuleBuilderOptions<T, TProperty?> GreaterThanZero<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder)
            where TProperty : struct, IComparable<TProperty>, IComparable, INumber<TProperty>
        {
            return ruleBuilder.SetValidator(new GreaterThanValidator<T, TProperty>(TProperty.Zero))
                .WithMessage((t, tProp) => GetValueGreaterThanErrorMessage(property: tProp, mustBeGreaterThan: TProperty.Zero, manualPropertyName: PropertyNameVariableQuotes));
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThanOrEqualToZero<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
            where TProperty : IComparable<TProperty>, IComparable, INumber<TProperty>
        {
            return ruleBuilder.SetValidator(new GreaterThanOrEqualValidator<T, TProperty>(TProperty.Zero))
                .WithMessage((t, tProp) => GetValueGreaterThanOrEqualToErrorMessage(property: tProp, mustBeGreaterThanOrEqualTo: TProperty.Zero, manualPropertyName: PropertyNameVariableQuotes));
        }

        public static IRuleBuilderOptions<T, TProperty?> GreaterThanOrEqualToZero<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder)
            where TProperty : struct, IComparable<TProperty>, IComparable, INumber<TProperty>
        {
            return ruleBuilder.SetValidator(new GreaterThanOrEqualValidator<T, TProperty>(TProperty.Zero))
                .WithMessage((t, tProp) => GetValueGreaterThanOrEqualToErrorMessage(property: tProp, mustBeGreaterThanOrEqualTo: TProperty.Zero, manualPropertyName: PropertyNameVariableQuotes));
        }

        public static IRuleBuilderOptions<T, TProperty> IsInEnum<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
            where TProperty : struct, Enum
        {
            return DefaultValidatorExtensions.IsInEnum(ruleBuilder)
                .WithMessage((t, tProp) => GetEnumErrorMessage<TProperty>());
        }

        public static string GetValueGreaterThanErrorMessage(object property, object mustBeGreaterThan, string manualPropertyName = null, [CallerArgumentExpression(Property)] string propertyName = null)
        {
            return string.Format(GreaterThanErrorMessage, manualPropertyName != null ? manualPropertyName : GetOnlyPropertyName(propertyName), mustBeGreaterThan?.ToString() ?? NULL);
        }

        public static string GetValueGreaterThanOrEqualToErrorMessage(object property, object mustBeGreaterThanOrEqualTo, string manualPropertyName = null, [CallerArgumentExpression(Property)] string propertyName = null)
        {
            return string.Format(GreaterThanOrEqualToErrorMessage, manualPropertyName != null ? manualPropertyName : GetOnlyPropertyName(propertyName), mustBeGreaterThanOrEqualTo?.ToString() ?? NULL);
        }

        public static string GetOnlyPropertyName(string fullName)
        {
            return fullName[(fullName.LastIndexOf('.') + 1)..];
        }

        private static string GetEnumErrorMessage<TEnum>()
            where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);

            var (values, names) = EnumValueAndNameCache.GetOrAdd(enumType, type =>
            {
                var valueList = Enum.GetValues(type)
                    .Cast<object>()
                    .Select(v => Convert.ToInt64(v, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture));

                var nameList = Enum.GetNames(type);

                return (string.Join(", ", valueList), string.Join(", ", nameList));
            });

            return string.Format(InvalidEnumMessage, PropertyNameVariableQuotes, values, names);
        }
    }
}


