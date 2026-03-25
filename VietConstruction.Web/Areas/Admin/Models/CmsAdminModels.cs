using System.ComponentModel.DataAnnotations;
using VietConstruction.Web.Data.Entities;

namespace VietConstruction.Web.Areas.Admin.Models;

public sealed class DashboardViewModel
{
    public int PageCount { get; init; }

    public int ProjectCount { get; init; }

    public int PostCount { get; init; }

    public int ContactCount { get; init; }

    public IReadOnlyList<DashboardItemViewModel> RecentProjects { get; init; } = Array.Empty<DashboardItemViewModel>();

    public IReadOnlyList<DashboardItemViewModel> RecentPosts { get; init; } = Array.Empty<DashboardItemViewModel>();

    public IReadOnlyList<DashboardItemViewModel> RecentContacts { get; init; } = Array.Empty<DashboardItemViewModel>();
}

public sealed class DashboardItemViewModel
{
    public required string Title { get; init; }

    public required string Meta { get; init; }
}

public sealed class AdminPagedListViewModel<TItem>
{
    public IReadOnlyList<TItem> Items { get; init; } = Array.Empty<TItem>();

    public int CurrentPage { get; init; }

    public int PageSize { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages { get; init; }
}

public sealed class PageEditorInput
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập slug.")]
    [StringLength(180)]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập nhãn menu.")]
    [StringLength(120)]
    public string NavigationTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mô tả SEO.")]
    [StringLength(300)]
    public string MetaDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tóm tắt.")]
    [StringLength(800)]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tiêu đề hero.")]
    [StringLength(220)]
    public string HeroTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mô tả hero.")]
    [StringLength(1000)]
    public string HeroSummary { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
    public string BodyHtml { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mã template.")]
    [StringLength(60)]
    public string TemplateKey { get; set; } = "page";

    public bool ShowInNavigation { get; set; }

    public int NavigationOrder { get; set; }

    public bool IsPublished { get; set; } = true;
}

public sealed class ProjectEditorInput
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên dự án.")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập slug.")]
    [StringLength(180)]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tóm tắt.")]
    [StringLength(800)]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
    public string BodyHtml { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập chủ đầu tư.")]
    [StringLength(180)]
    public string ClientName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập địa điểm.")]
    [StringLength(220)]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập quy mô.")]
    [StringLength(220)]
    public string Scale { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập đường dẫn ảnh.")]
    [StringLength(400)]
    public string FeaturedImageUrl { get; set; } = string.Empty;

    public bool IsFeatured { get; set; }

    public bool IsPublished { get; set; } = true;

    public ProjectStatus Status { get; set; }

    public int DisplayOrder { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? StartedOn { get; set; }

    [DataType(DataType.Date)]
    public DateOnly? CompletedOn { get; set; }
}

public sealed class CategoryEditorInput
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên danh mục.")]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập slug.")]
    [StringLength(140)]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mô tả.")]
    [StringLength(400)]
    public string Description { get; set; } = string.Empty;

    public PostType PostType { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsVisible { get; set; } = true;
}

public sealed class PostEditorInput
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
    [StringLength(220)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập slug.")]
    [StringLength(180)]
    public string Slug { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tóm tắt.")]
    [StringLength(800)]
    public string Summary { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
    public string BodyHtml { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mô tả SEO.")]
    [StringLength(300)]
    public string MetaDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập ảnh đại diện.")]
    [StringLength(400)]
    public string FeaturedImageUrl { get; set; } = string.Empty;

    public PostType PostType { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục.")]
    public int CategoryId { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsPublished { get; set; } = true;

    [DataType(DataType.DateTime)]
    public DateTime PublishedAtUtc { get; set; } = DateTime.UtcNow;

    [StringLength(180)]
    public string JobLocation { get; set; } = string.Empty;

    [StringLength(180)]
    public string ExperienceRequirement { get; set; } = string.Empty;

    [StringLength(180)]
    public string SalaryRange { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateOnly? ApplicationDeadline { get; set; }
}

public sealed class ContactResolutionInput
{
    public int Id { get; set; }

    public bool IsResolved { get; set; }

    [StringLength(600)]
    public string ResolutionNote { get; set; } = string.Empty;
}

public sealed class UserEditorInput
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email chưa đúng định dạng.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(30, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn vai trò.")]
    public string Role { get; set; } = "Editor";

    public bool IsActive { get; set; } = true;
}
