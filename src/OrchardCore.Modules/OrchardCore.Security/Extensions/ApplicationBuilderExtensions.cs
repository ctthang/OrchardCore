using System;
using OrchardCore.Security;
using OrchardCore.Security.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseReferrerPolicy(this IApplicationBuilder app, ReferrerPolicy policy)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ReferrerPolicyMiddleware>(policy);
        }

        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseSecurityHeaders(new SecurityHeadersOptions());
        }

        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, SecurityHeadersOptions options)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseReferrerPolicy(options.ReferrerPolicy);
        }

        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, Action<SecurityHeadersOptions> optionsAction)
        {
            if (app is null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (optionsAction is null)
            {
                throw new ArgumentNullException(nameof(optionsAction));
            }

            var options = new SecurityHeadersOptions();
            optionsAction.Invoke(options);

            return app.UseSecurityHeaders(options);
        }
    }
}
