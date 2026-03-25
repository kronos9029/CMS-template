using System.ComponentModel.DataAnnotations;

namespace VietConstruction.Web.Models;

public sealed record ActionLink(string Label, string Url, bool IsPrimary = false);

public sealed record MetricItem(string Value, string Label);

public sealed record HeroProofItem(string Value, string Eyebrow, string Title);

public sealed record KeyValueItem(string Label, string Value);

public sealed record NavigationItem(string Label, string Url, IReadOnlyList<NavigationItem>? Children = null);

public sealed record FooterLinkGroup(string Title, IReadOnlyList<ActionLink> Links);

public sealed record CompanyContact(
    string CompanyName,
    string Tagline,
    string Address,
    string Hotline,
    string Email,
    string Website,
    string WorkingHours);

public sealed class SiteLayoutData
{
    public required IReadOnlyList<NavigationItem> Navigation { get; init; }

    public required IReadOnlyList<FooterLinkGroup> FooterGroups { get; init; }

    public required CompanyContact Contact { get; init; }

    public required IReadOnlyList<string> FooterCredentials { get; init; }
}

public sealed class ShowcaseCardViewModel
{
    public required string Title { get; init; }

    public required string Summary { get; init; }

    public string? Meta { get; init; }

    public string? Tag { get; init; }

    public string? ImageUrl { get; init; }

    public string? LinkUrl { get; init; }

    public string? LinkLabel { get; init; }

    public IReadOnlyList<KeyValueItem> Facts { get; init; } = Array.Empty<KeyValueItem>();
}

public sealed class PageHeroViewModel
{
    public required string Eyebrow { get; init; }

    public required string Title { get; init; }

    public required string Summary { get; init; }

    public IReadOnlyList<MetricItem> Metrics { get; init; } = Array.Empty<MetricItem>();

    public IReadOnlyList<ActionLink> Actions { get; init; } = Array.Empty<ActionLink>();
}

public sealed class ContentSectionViewModel
{
    public required string AnchorId { get; init; }

    public required string Title { get; init; }

    public string? Summary { get; init; }

    public string? HtmlContent { get; init; }

    public IReadOnlyList<string> Paragraphs { get; init; } = Array.Empty<string>();

    public IReadOnlyList<string> BulletItems { get; init; } = Array.Empty<string>();

    public IReadOnlyList<KeyValueItem> FactItems { get; init; } = Array.Empty<KeyValueItem>();
}

public sealed class CorporatePageViewModel
{
    public required string Title { get; init; }

    public required string MetaDescription { get; init; }

    public required string Eyebrow { get; init; }

    public required string HeroTitle { get; init; }

    public required string HeroSummary { get; init; }

    public IReadOnlyList<MetricItem> HeroMetrics { get; init; } = Array.Empty<MetricItem>();

    public IReadOnlyList<ActionLink> HeroActions { get; init; } = Array.Empty<ActionLink>();

    public IReadOnlyList<ContentSectionViewModel> Sections { get; init; } = Array.Empty<ContentSectionViewModel>();

    public IReadOnlyList<ShowcaseCardViewModel> Highlights { get; init; } = Array.Empty<ShowcaseCardViewModel>();
}

public sealed class ProjectCardViewModel
{
    public required string Title { get; init; }

    public required string Status { get; init; }

    public required string Location { get; init; }

    public required string Client { get; init; }

    public required string Scale { get; init; }

    public required string Summary { get; init; }

    public required string ImageUrl { get; init; }
}

public sealed class ProjectListingPageViewModel
{
    public required string Title { get; init; }

    public required string MetaDescription { get; init; }

    public required string Eyebrow { get; init; }

    public required string HeroTitle { get; init; }

    public required string HeroSummary { get; init; }

    public required string CurrentFilter { get; init; }

    public IReadOnlyList<ActionLink> HeroActions { get; init; } = Array.Empty<ActionLink>();

    public IReadOnlyList<MetricItem> HeroMetrics { get; init; } = Array.Empty<MetricItem>();

    public IReadOnlyList<ProjectCardViewModel> Projects { get; init; } = Array.Empty<ProjectCardViewModel>();

    public int CurrentPage { get; init; }

    public int PageSize { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages { get; init; }
}

public sealed class ArticleCardViewModel
{
    public required string Title { get; init; }

    public required string Category { get; init; }

    public required DateOnly PublishedOn { get; init; }

    public required string Summary { get; init; }

    public required string ImageUrl { get; init; }
}

public sealed class NewsListingPageViewModel
{
    public required string Title { get; init; }

    public required string MetaDescription { get; init; }

    public required string Eyebrow { get; init; }

    public required string HeroTitle { get; init; }

    public required string HeroSummary { get; init; }

    public required string CurrentCategory { get; init; }

    public IReadOnlyList<ActionLink> HeroActions { get; init; } = Array.Empty<ActionLink>();

    public IReadOnlyList<ArticleCardViewModel> Articles { get; init; } = Array.Empty<ArticleCardViewModel>();

    public int CurrentPage { get; init; }

    public int PageSize { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages { get; init; }
}

public sealed class JobCardViewModel
{
    public required string Title { get; init; }

    public required string Location { get; init; }

    public required string Experience { get; init; }

    public required string Salary { get; init; }

    public required DateOnly Deadline { get; init; }

    public required string Summary { get; init; }
}

public sealed class RecruitmentListingPageViewModel
{
    public required string Title { get; init; }

    public required string MetaDescription { get; init; }

    public required string Eyebrow { get; init; }

    public required string HeroTitle { get; init; }

    public required string HeroSummary { get; init; }

    public IReadOnlyList<ActionLink> HeroActions { get; init; } = Array.Empty<ActionLink>();

    public IReadOnlyList<JobCardViewModel> Jobs { get; init; } = Array.Empty<JobCardViewModel>();

    public int CurrentPage { get; init; }

    public int PageSize { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages { get; init; }
}

public sealed class OfficeCardViewModel
{
    public required string Name { get; init; }

    public required string Address { get; init; }

    public required string Hotline { get; init; }

    public required string Email { get; init; }

    public required string WorkingHours { get; init; }
}

public sealed class ContactPageViewModel
{
    public required string Title { get; init; }

    public required string MetaDescription { get; init; }

    public required string Eyebrow { get; init; }

    public required string HeroTitle { get; init; }

    public required string HeroSummary { get; init; }

    public IReadOnlyList<OfficeCardViewModel> Offices { get; init; } = Array.Empty<OfficeCardViewModel>();

    public IReadOnlyList<KeyValueItem> SupportItems { get; init; } = Array.Empty<KeyValueItem>();

    public ContactFormInputModel Form { get; init; } = new();

    public bool IsSubmitted { get; init; }

    public string? SubmissionMessage { get; init; }
}

public sealed class HomePageViewModel
{
    public required string Title { get; init; }

    public required string MetaDescription { get; init; }

    public required string HeroTitle { get; init; }

    public required string HeroSummary { get; init; }

    public IReadOnlyList<ActionLink> HeroActions { get; init; } = Array.Empty<ActionLink>();

    public IReadOnlyList<MetricItem> HeroMetrics { get; init; } = Array.Empty<MetricItem>();

    public required string IntroTitle { get; init; }

    public required string IntroSummary { get; init; }

    public IReadOnlyList<string> IntroHighlights { get; init; } = Array.Empty<string>();

    public IReadOnlyList<ShowcaseCardViewModel> CapabilityHighlights { get; init; } = Array.Empty<ShowcaseCardViewModel>();

    public IReadOnlyList<ProjectCardViewModel> FeaturedProjects { get; init; } = Array.Empty<ProjectCardViewModel>();

    public required HeroProofItem HeroProof { get; init; }

    public IReadOnlyList<ArticleCardViewModel> LatestNews { get; init; } = Array.Empty<ArticleCardViewModel>();

    public IReadOnlyList<JobCardViewModel> RecruitmentPreview { get; init; } = Array.Empty<JobCardViewModel>();
}

public sealed class ContactFormInputModel
{
    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(120)]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [Phone(ErrorMessage = "Số điện thoại chưa đúng định dạng.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Địa chỉ email chưa đúng định dạng.")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Nội dung liên hệ")]
    [Required(ErrorMessage = "Vui lòng nhập nội dung liên hệ.")]
    [StringLength(2000)]
    public string Message { get; set; } = string.Empty;
}
