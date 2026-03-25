namespace VietConstruction.Web.Data.Entities;

public sealed class ContactSubmission : AuditableEntity
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public bool IsResolved { get; set; }

    public string ResolutionNote { get; set; } = string.Empty;
}
