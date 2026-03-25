namespace VietConstruction.Web.Options;

public sealed class CmsSeedOptions
{
    public const string SectionName = "CmsSeed";

    public bool Enabled { get; set; } = true;

    public string AdminEmail { get; set; } = "admin@vietconstruction.local";

    public string AdminPassword { get; set; } = "Admin@123456";

    public string AdminFullName { get; set; } = "Quản trị hệ thống";

    public string EditorEmail { get; set; } = "editor@vietconstruction.local";

    public string EditorPassword { get; set; } = "Editor@123456";

    public string EditorFullName { get; set; } = "Biên tập nội dung";
}
