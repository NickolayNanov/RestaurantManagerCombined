using System.Numerics;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Validators;

namespace RestaurantManager.Application
{
    public abstract class ApplicationValidator<T> : AbstractValidator<T>
    {
        
    }
}
