using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Security.Options;
using Xunit;

namespace OrchardCore.Security.Extensions.Tests
{
    public class SecurityHeadersApplicationBuilderExtensionsTests
    {
        [Fact]
        public void UseSecurityHeadersWithDefaultHeaders()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var applicationBuilder = CreateApplicationBuilder();

            // Act
            applicationBuilder.UseSecurityHeaders();

            applicationBuilder
                .Build()
                .Invoke(context);

            // Assert
            Assert.Equal(ContentTypeOptionsValue.NoSniff, context.Response.Headers[SecurityHeaderNames.XContentTypeOptions]);
            Assert.Equal(SecurityHeaderDefaults.FrameOptions, context.Response.Headers[SecurityHeaderNames.XFrameOptions]);
            Assert.Equal("accelerometer=(), ambient-light-sensor=(), autoplay=(), camera=(), encrypted-media=(), fullscreen=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), midi=(), notifications=(), payment=(), picture-in-picture=(), push=(), speaker=(), sync-xhr=(), usb=(), vibrate=(), vr=()", context.Response.Headers[SecurityHeaderNames.PermissionsPolicy]);
            Assert.Equal(SecurityHeaderDefaults.ReferrerPolicy, context.Response.Headers[SecurityHeaderNames.ReferrerPolicy]);
        }

        [Fact]
        public void UseSecurityHeadersWithOptions()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var options = new SecurityHeadersOptions
            {
                ContentTypeOptions = ContentTypeOptionsValue.NoSniff,
                FrameOptions = FrameOptionsValue.Deny,
                PermissionsPolicy = new []
                {
                    $"{PermissionsPolicyValue.Camera}={PermissionsPolicyOriginValue.Self}",
                    $"{PermissionsPolicyValue.Microphone}={PermissionsPolicyOriginValue.Any}"
                },
                ReferrerPolicy = ReferrerPolicyValue.Origin
            };
            var applicationBuilder = CreateApplicationBuilder();

            // Act
            applicationBuilder.UseSecurityHeaders(options);

            applicationBuilder
                .Build()
                .Invoke(context);

            // Assert
            Assert.Equal(ContentTypeOptionsValue.NoSniff, context.Response.Headers[SecurityHeaderNames.XContentTypeOptions]);
            Assert.Equal(FrameOptionsValue.Deny, context.Response.Headers[SecurityHeaderNames.XFrameOptions]);
            Assert.Equal("camera=self, microphone=*", context.Response.Headers[SecurityHeaderNames.PermissionsPolicy]);
            Assert.Equal(ReferrerPolicyValue.Origin, context.Response.Headers[SecurityHeaderNames.ReferrerPolicy]);
        }

        [Fact]
        public void UseSecurityHeadersWithBuilderConfiguration()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var applicationBuilder = CreateApplicationBuilder();

            // Act
            applicationBuilder.UseSecurityHeaders(config =>
            {
                config
                    .AddContentTypeOptions()
                    .AddFrameOptions()
                        .WithDeny()
                    .AddPermissionsPolicy("camera=self", "microphone=*")
                    //.AddPermissionsPolicy(options =>
                    //{
                    //    options
                    //        .AllowCamera(PermissionsPolicyOriginValue.Self)
                    //        .AllowMicrophone(PermissionsPolicyOriginValue.Any);
                    //})
                    .AddReferrerPolicy()
                        .WithOrigin();
            });

            applicationBuilder
                .Build()
                .Invoke(context);

            // Assert
            Assert.Equal(ContentTypeOptionsValue.NoSniff, context.Response.Headers[SecurityHeaderNames.XContentTypeOptions]);
            Assert.Equal(FrameOptionsValue.Deny, context.Response.Headers[SecurityHeaderNames.XFrameOptions]);
            Assert.Equal("camera=self, microphone=*", context.Response.Headers[SecurityHeaderNames.PermissionsPolicy]);
            Assert.Equal(ReferrerPolicyValue.Origin, context.Response.Headers[SecurityHeaderNames.ReferrerPolicy]);
        }

        private static IApplicationBuilder CreateApplicationBuilder()
        {
            var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();

            return new ApplicationBuilder(serviceProvider);
        }
    }
}
