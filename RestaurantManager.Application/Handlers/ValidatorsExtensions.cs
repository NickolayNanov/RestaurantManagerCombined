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
        public const string InvalidMaxLengthMessage = "The length of {0} must be {1} characters or fewer. You entered {2} characters.";
        public const string InvalidEmptyInputArray = "The array of {0} cannot be empty.";
        public const string InvalidNullInputArray = "The array of {0} cannot be null.";
        public const string RequiredErrorMessage = "The field {0} is required.";
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
                    var guidValue = (Guid?)(object?)value;
                    return !guidValue.HasValue || guidValue.Value != Guid.Empty;
                })
                .WithMessage(string.Format(RequiredErrorMessage, "{PropertyName}"));
            }

            return builder;
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
            return ruleBuilder.IsInEnum()
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



