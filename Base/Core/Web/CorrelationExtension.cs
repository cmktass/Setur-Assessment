using Microsoft.AspNetCore.Builder;

namespace Core.Web
{
    public static class CorrelationExtension
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.TryGetValue("CorrelationId", out var correlationId))
                    correlationId = Guid.NewGuid().ToString();

                context.Items["CorrelationId"] = correlationId.ToString();
                await next();
            });
        }
    }
}
