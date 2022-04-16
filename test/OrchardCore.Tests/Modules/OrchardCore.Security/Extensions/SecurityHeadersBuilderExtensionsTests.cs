using Xunit;

namespace OrchardCore.Security.Extensions.Tests
{
    public class SecurityHeadersBuilderExtensionsTests
    {
        [Fact]
        public void AddContentTypeOptionsWithFluentAPIsConfiguration()
        {
            // Arrange
            var settings = new SecuritySettings();
            var builder = new SecurityHeadersBuilder(settings);

            // Act
            builder
                .AddContentTypeOptions()
                .WithNoSniff();

            // Assert
            Assert.NotNull(settings);
            Assert.Equal(ContentTypeOptions.NoSniff, settings.ContentTypeOptions);
        }

        [Fact]
        public void AddFrameOptionsWithFluentAPIsConfiguration()
        {
            // Arrange
            var settings = new SecuritySettings();
            var builder = new SecurityHeadersBuilder(settings);

            // Act
            builder
                .AddFrameOptions()
                .WithSameOrigin();

            // Assert
            Assert.NotNull(settings);
            Assert.Equal(FrameOptions.SameOrigin, settings.FrameOptions);
        }

        [Fact]
        public void AddPermissionsPolicyWithFluentAPIsConfiguration()
        {
            // Arrange
            var settings = new SecuritySettings();
            var builder = new SecurityHeadersBuilder(settings);

            // Act
            builder
                .AddPermissionsPolicy()
                .WithCamera()
                .WithMicrophone();

            // Assert
            Assert.NotNull(settings);
            Assert.Contains(PermissionsPolicyOptions.Camera, settings.PermissionsPolicy);
            Assert.Contains(PermissionsPolicyOptions.Microphone, settings.PermissionsPolicy);
        }

        [Fact]
        public void AddReferrerPolicyWithFluentAPIsConfiguration()
        {
            // Arrange
            var settings = new SecuritySettings();
            var builder = new SecurityHeadersBuilder(settings);

            // Act
            builder
                .AddReferrerPolicy()
                .WithSameOrigin();

            // Assert
            Assert.NotNull(settings);
            Assert.Equal(ReferrerPolicyOptions.SameOrigin, settings.ReferrerPolicy);
        }

        [Fact]
        public void AddSecurityHeadersWithFluentAPIsConfiguration()
        {
            // Arrange
            var settings = new SecuritySettings();
            var builder = new SecurityHeadersBuilder(settings);

            // Act
            builder
                .AddContentTypeOptions()
                    .WithNoSniff()
                .AddFrameOptions()
                    .WithSameOrigin()
                .AddPermissionsPolicy()
                    .WithCamera()
                    .WithMicrophone()
                    .Build()
                .AddReferrerPolicy()
                    .WithSameOrigin();

            // Assert
            Assert.NotNull(settings);
            Assert.Equal(ContentTypeOptions.NoSniff, settings.ContentTypeOptions);
            Assert.Equal(FrameOptions.SameOrigin, settings.FrameOptions);
            Assert.Contains(PermissionsPolicyOptions.Camera, settings.PermissionsPolicy);
            Assert.Contains(PermissionsPolicyOptions.Microphone, settings.PermissionsPolicy);
            Assert.Equal(ReferrerPolicyOptions.SameOrigin, settings.ReferrerPolicy);
        }
    }
}
