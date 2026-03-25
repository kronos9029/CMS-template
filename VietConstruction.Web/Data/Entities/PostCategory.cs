namespace VietConstruction.Web.Data.Entities;

public sealed class PostCategory : AuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public PostType PostType { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsVisible { get; set; } = true;

    public ICollection<ContentPost> Posts { get; set; } = new List<ContentPost>();
}
