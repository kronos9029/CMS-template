namespace VietConstruction.Web.Data.Entities;

public sealed class ContentPost : AuditableEntity
{
    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string BodyHtml { get; set; } = string.Empty;

    public string MetaDescription { get; set; } = string.Empty;

    public string FeaturedImageUrl { get; set; } = string.Empty;

    public PostType PostType { get; set; }

    public int CategoryId { get; set; }

    public PostCategory? Category { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsPublished { get; set; } = true;

    public DateTime PublishedAtUtc { get; set; } = DateTime.UtcNow;

    public string JobLocation { get; set; } = string.Empty;

    public string ExperienceRequirement { get; set; } = string.Empty;

    public string SalaryRange { get; set; } = string.Empty;

    public DateOnly? ApplicationDeadline { get; set; }
}
