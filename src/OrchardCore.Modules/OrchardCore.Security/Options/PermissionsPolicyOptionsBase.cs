namespace OrchardCore.Security.Options
{
    public abstract class PermissionsPolicyOptionsBase
    {
        public abstract string Name { get; }

        public string Origin { get; set; } = PermissionsPolicyOriginValue.None;
    }
}