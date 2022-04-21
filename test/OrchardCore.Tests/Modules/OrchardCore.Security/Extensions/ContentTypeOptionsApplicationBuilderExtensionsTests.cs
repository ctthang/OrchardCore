using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Security.Options;
using Xunit;

namespace OrchardCore.Security.Extensions.Tests
{
    public class ContentTypeOptionsApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseContentTypeOptionsWithoutOptionsInjectDefaultHeader()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var applicationBuilder = CreateApplicationBuilder();

            // Act
            applicationBuilder.UseContentTypeOptions();

            applicationBuilder
                .Build()
                .Invoke(context);

            // Assert
            Assert.Equal(SecurityHeaderDefaults.ContentTypeOptions, context.Response.Headers[SecurityHeaderNames.XContentTypeOptions]);
        }

        [Fact]
        public void UseContentTypeOptionsWithOptions()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var options = new ContentTypeOptionsOptions();
            var applicationBuilder = CreateApplicationBuilder();

            // Act
            applicationBuilder.UseContentTypeOptions(options);

            applicationBuilder
                .Build()
                .Invoke(context);

            // Assert
            Assert.Equal(ContentTypeOptionsValue.NoSniff, context.Response.Headers[SecurityHeaderNames.XContentTypeOptions]);
        }

        private static IApplicationBuilder CreateApplicationBuilder()
        {
            var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();

            return new ApplicationBuilder(serviceProvider);
        }
    }
}
