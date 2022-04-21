using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OrchardCore.Security.Options;

namespace OrchardCore.Security.Middlewares
{
    public class ContentTypeOptionsMiddleware
    {
        private readonly ContentTypeOptionsOptions _options;
        private readonly RequestDelegate _next;

        public ContentTypeOptionsMiddleware(IOptions<ContentTypeOptionsOptions> options, RequestDelegate next)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options.Value;
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers[SecurityHeaderNames.XContentTypeOptions] = _options.Value;

            return _next.Invoke(context);
        }
    }
}
