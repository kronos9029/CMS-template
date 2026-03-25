namespace VietConstruction.Web.Data.Entities;

public sealed class SiteSetting : AuditableEntity
{
    public string CompanyName { get; set; } = string.Empty;

    public string Tagline { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Hotline { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Website { get; set; } = string.Empty;

    public string WorkingHours { get; set; } = string.Empty;

    public string FooterCredentials { get; set; } = string.Empty;

    public string HeroTitle { get; set; } = string.Empty;

    public string HeroSummary { get; set; } = string.Empty;
}
