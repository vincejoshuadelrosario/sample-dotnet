namespace Sample.HealthChecks.Web.Api.Configurations
{
    public sealed class EnvironmentSettings
    {
        public const string SectionName = "Environments";

        public Uri Integration01BaseUrl { get; set; }

        public Uri Integration02BaseUrl { get; set; }

        public Uri Integration03BaseUrl { get; set; }
    }
}
