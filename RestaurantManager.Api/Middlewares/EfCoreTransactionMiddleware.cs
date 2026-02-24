using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantManager.Infrastructure;

namespace RestaurantManager.Api.Middlewares
{
    public sealed class EfCoreTransactionMiddleware(ILogger<EfCoreTransactionMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var method = context.Request.Method;

            var isRead = HttpMethods.IsGet(method) || HttpMethods.IsHead(method) || HttpMethods.IsOptions(method);
            
            // Only wrap "write" requests
            if (isRead)
            {
                await next(context);
                return;
            }

            var db = context.RequestServices.GetRequiredService<RestaurantManagerDbContext>();

            // If something upstream already opened a transaction, just continue
            if (db.Database.CurrentTransaction is not null)
            {
                await next(context);
                return;
            }

            // Execution strategy is important for SQL Server retries, etc.
            var strategy = db.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await db.Database.BeginTransactionAsync(context.RequestAborted);

                try
                {
                    await next(context);

                    // Commit if no exception happened
                    await db.SaveChangesAsync(context.RequestAborted);
                    await tx.CommitAsync(context.RequestAborted);

                    logger.LogInformation($"Commited transaction {tx.TransactionId} successfully.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during request processing. Rolling back transaction.");
                    await tx.RollbackAsync(context.RequestAborted);

                    throw;
                }
            });
        }
    }
}
