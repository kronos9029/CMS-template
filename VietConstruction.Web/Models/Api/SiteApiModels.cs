using System.ComponentModel.DataAnnotations;

namespace VietConstruction.Web.Models.Api;

public sealed class SiteNavigationResponse
{
    public required SiteSettingsResponse Site { get; init; }

    public IReadOnlyList<NavigationResponse> Navigation { get; init; } = Array.Empty<NavigationResponse>();
}

public sealed class NavigationResponse
{
    public required string Title { get; init; }

    public required string Slug { get; init; }
}

public sealed class SiteSettingsResponse
{
    public required string CompanyName { get; init; }

    public required string Tagline { get; init; }

    public required string Address { get; init; }

    public required string Hotline { get; init; }

    public required string Email { get; init; }

    public required string Website { get; init; }

    public required string WorkingHours { get; init; }

    public required string FooterCredentials { get; init; }
}

public sealed class HomeResponse
{
    public required SiteSettingsResponse Site { get; init; }

    public required PageResponse HomePage { get; init; }

    public IReadOnlyList<ProjectResponse> FeaturedProjects { get; init; } = Array.Empty<ProjectResponse>();

    public IReadOnlyList<PostResponse> LatestNews { get; init; } = Array.Empty<PostResponse>();

    public IReadOnlyList<PostResponse> RecruitmentPreview { get; init; } = Array.Empty<PostResponse>();
}

public sealed class PageResponse
{
    public int Id { get; init; }

    public required string Title { get; init; }

    public required string Slug { get; init; }

    public required string NavigationTitle { get; init; }

    public required string Summary { get; init; }

    public required string HeroTitle { get; init; }

    public required string HeroSummary { get; init; }

    public required string BodyHtml { get; init; }

    public required string MetaDescription { get; init; }

    public required string TemplateKey { get; init; }
}

public sealed class ProjectResponse
{
    public int Id { get; init; }

    public required string Title { get; init; }

    public required string Slug { get; init; }

    public required string Summary { get; init; }

    public required string BodyHtml { get; init; }

    public required string ClientName { get; init; }

    public required string Location { get; init; }

    public required string Scale { get; init; }

    public required string FeaturedImageUrl { get; init; }

    public required string Status { get; init; }

    public bool IsFeatured { get; init; }

    public DateOnly? StartedOn { get; init; }

    public DateOnly? CompletedOn { get; init; }
}

public sealed class PostResponse
{
    public int Id { get; init; }

    public required string Title { get; init; }

    public required string Slug { get; init; }

    public required string Summary { get; init; }

    public required string BodyHtml { get; init; }

    public required string MetaDescription { get; init; }

    public required string FeaturedImageUrl { get; init; }

    public required string PostType { get; init; }

    public required string CategoryName { get; init; }

    public bool IsFeatured { get; init; }

    public DateTime PublishedAtUtc { get; init; }

    public required string JobLocation { get; init; }

    public required string ExperienceRequirement { get; init; }

    public required string SalaryRange { get; init; }

    public DateOnly? ApplicationDeadline { get; init; }
}

public sealed class ContactSubmissionRequest
{
    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email chưa đúng định dạng.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập nội dung.")]
    public string Message { get; set; } = string.Empty;
}
