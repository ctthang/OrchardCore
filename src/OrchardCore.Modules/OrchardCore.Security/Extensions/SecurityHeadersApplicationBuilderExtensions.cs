using System;
using Microsoft.Extensions.Options;
using OrchardCore.Security;
using OrchardCore.Security.Options;

namespace Microsoft.AspNetCore.Builder
{
    public static class SecurityHeadersApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            return app.UseSecurityHeaders(new SecurityHeadersOptions());
        }

        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, SecurityHeadersOptions options)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            app.UseMiddleware<SecurityHeadersMiddleware>(Options.Create(options));

            return app;
        }

        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, Action<SecurityHeadersOptions> optionsAction)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));
            ArgumentNullException.ThrowIfNull(optionsAction, nameof(optionsAction));

            var options = new SecurityHeadersOptions();

            optionsAction.Invoke(options);

            return app.UseSecurityHeaders(options);
        }
    }
}
