using Microsoft.AspNetCore.Identity;

namespace VietConstruction.Web.Data.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
