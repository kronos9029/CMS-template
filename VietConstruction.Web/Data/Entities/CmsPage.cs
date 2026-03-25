namespace VietConstruction.Web.Data.Entities;

public sealed class CmsPage : AuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string NavigationTitle { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string HeroTitle { get; set; } = string.Empty;

    public string HeroSummary { get; set; } = string.Empty;

    public string BodyHtml { get; set; } = string.Empty;

    public string TemplateKey { get; set; } = string.Empty;

    public bool ShowInNavigation { get; set; }

    public int NavigationOrder { get; set; }

    public bool IsPublished { get; set; } = true;
}
