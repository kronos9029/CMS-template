namespace VietConstruction.Web.Data.Entities;

public abstract class AuditableEntity
{
    public int Id { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime UpdatedAtUtc { get; set; }
}
