namespace VietConstruction.Web.Data.Entities;

public sealed class ProjectEntry : AuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string BodyHtml { get; set; } = string.Empty;

    public string ClientName { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string Scale { get; set; } = string.Empty;

    public string FeaturedImageUrl { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }

    public bool IsPublished { get; set; } = true;

    public ProjectStatus Status { get; set; }

    public int DisplayOrder { get; set; }

    public DateOnly? StartedOn { get; set; }

    public DateOnly? CompletedOn { get; set; }
}
