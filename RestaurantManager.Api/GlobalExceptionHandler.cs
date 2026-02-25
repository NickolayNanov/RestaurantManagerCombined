using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RestaurantManager.Application.Exceptions;

namespace RestaurantManager.Api
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(IProblemDetailsService problemDetailsService, IHostEnvironment env)
        {
            _problemDetailsService = problemDetailsService;
            _env = env;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
        {
            // If the response already started, we can’t write our own body safely
            if (httpContext.Response.HasStarted)
                return false;

            ProblemDetails problem;

            switch (exception)
            {
                case ValidationException validationException:
                    {
                        var errors = validationException.Errors
                            .GroupBy(e => e.PropertyName)
                            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                        problem = new ValidationProblemDetails(errors)
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Title = "Validation failed",
                            Type = "https://httpstatuses.com/400",
                            Instance = httpContext.Request.Path
                        };
                        break;
                    }

                case ResourceNotFoundException resourceNotFoundException:
                    problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = $"{resourceNotFoundException.ResourceName} Resource not found",
                        Type = "https://httpstatuses.com/404",
                        Detail = resourceNotFoundException.Message,
                        Instance = httpContext.Request.Path
                    };
                    break;

                case UnauthorizedAccessException:
                    problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = "Unauthorized",
                        Type = "https://httpstatuses.com/401",
                        Instance = httpContext.Request.Path
                    };
                    break;
                case InvalidOperationException:
                    problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Invalid operation",
                        Type = "https://httpstatuses.com/400",
                        Detail = exception.Message,
                        Instance = httpContext.Request.Path
                    };
                    break;

                // Example custom exceptions:
                // case NotFoundException nf:
                //     problem = new ProblemDetails { Status = 404, Title = "Not found", Detail = nf.Message, Type = "https://httpstatuses.com/404" };
                //     break;

                default:
                    problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Unexpected error",
                        Type = "https://httpstatuses.com/500",
                        Detail = _env.IsDevelopment() ? exception.ToString() : "Something went wrong.",
                        Instance = httpContext.Request.Path
                    };
                    break;
            }

            httpContext.Response.StatusCode = problem.Status ?? 500;

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem,
                Exception = exception
            });
        }
    }
}
