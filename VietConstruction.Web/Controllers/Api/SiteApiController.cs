using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VietConstruction.Web.Data;
using VietConstruction.Web.Data.Entities;
using VietConstruction.Web.Models.Api;

namespace VietConstruction.Web.Controllers.Api;

[ApiController]
[Route("api/site")]
public sealed class SiteApiController(ApplicationDbContext dbContext) : ControllerBase
{
    [HttpGet("navigation")]
    public async Task<ActionResult<SiteNavigationResponse>> GetNavigation()
    {
        var settings = await GetSiteSettingsAsync();
        var navigation = await dbContext.Pages
            .Where(x => x.ShowInNavigation && x.IsPublished)
            .OrderBy(x => x.NavigationOrder)
            .Select(x => new NavigationResponse
            {
                Title = x.NavigationTitle,
                Slug = x.Slug
            })
            .ToListAsync();

        return Ok(new SiteNavigationResponse
        {
            Site = settings,
            Navigation = navigation
        });
    }

    [HttpGet("home")]
    public async Task<ActionResult<HomeResponse>> GetHome()
    {
        var settings = await GetSiteSettingsAsync();
        var homePage = await dbContext.Pages
            .Where(x => x.Slug == "trang-chu" && x.IsPublished)
            .Select(ProjectPage)
            .SingleOrDefaultAsync();

        if (homePage is null)
        {
            return NotFound();
        }

        var featuredProjects = await dbContext.Projects
            .Where(x => x.IsPublished && x.IsFeatured)
            .OrderBy(x => x.DisplayOrder)
            .Take(6)
            .Select(ProjectProject)
            .ToListAsync();

        var latestNews = await dbContext.Posts
            .Include(x => x.Category)
            .Where(x => x.IsPublished && x.PostType != PostType.Recruitment)
            .OrderByDescending(x => x.PublishedAtUtc)
            .Take(4)
            .Select(ProjectPost)
            .ToListAsync();

        var recruitmentPreview = await dbContext.Posts
            .Include(x => x.Category)
            .Where(x => x.IsPublished && x.PostType == PostType.Recruitment)
            .OrderByDescending(x => x.PublishedAtUtc)
            .Take(3)
            .Select(ProjectPost)
            .ToListAsync();

        return Ok(new HomeResponse
        {
            Site = settings,
            HomePage = homePage,
            FeaturedProjects = featuredProjects,
            LatestNews = latestNews,
            RecruitmentPreview = recruitmentPreview
        });
    }

    [HttpGet("pages/{slug}")]
    public async Task<ActionResult<PageResponse>> GetPage(string slug)
    {
        var page = await dbContext.Pages
            .Where(x => x.Slug == slug && x.IsPublished)
            .Select(ProjectPage)
            .SingleOrDefaultAsync();

        return page is null ? NotFound() : Ok(page);
    }

    [HttpGet("projects")]
    public async Task<ActionResult<IReadOnlyList<ProjectResponse>>> GetProjects([FromQuery] string? status = null, [FromQuery] bool? featured = null)
    {
        var query = dbContext.Projects.Where(x => x.IsPublished);

        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<ProjectStatus>(status, true, out var parsedStatus))
        {
            query = query.Where(x => x.Status == parsedStatus);
        }

        if (featured.HasValue)
        {
            query = query.Where(x => x.IsFeatured == featured.Value);
        }

        var projects = await query
            .OrderBy(x => x.DisplayOrder)
            .ThenByDescending(x => x.UpdatedAtUtc)
            .Select(ProjectProject)
            .ToListAsync();

        return Ok(projects);
    }

    [HttpGet("posts")]
    public async Task<ActionResult<IReadOnlyList<PostResponse>>> GetPosts([FromQuery] string? type = null, [FromQuery] string? category = null, [FromQuery] bool? featured = null)
    {
        var query = dbContext.Posts.Include(x => x.Category).Where(x => x.IsPublished);

        if (!string.IsNullOrWhiteSpace(type) && Enum.TryParse<PostType>(type, true, out var parsedType))
        {
            query = query.Where(x => x.PostType == parsedType);
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(x => x.Category != null && x.Category.Slug == category);
        }

        if (featured.HasValue)
        {
            query = query.Where(x => x.IsFeatured == featured.Value);
        }

        var posts = await query
            .OrderByDescending(x => x.PublishedAtUtc)
            .Select(ProjectPost)
            .ToListAsync();

        return Ok(posts);
    }

    [HttpPost("contact")]
    public async Task<IActionResult> SubmitContact([FromBody] ContactSubmissionRequest input)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        dbContext.ContactSubmissions.Add(new ContactSubmission
        {
            FullName = input.FullName.Trim(),
            Email = input.Email.Trim(),
            PhoneNumber = input.PhoneNumber.Trim(),
            Message = input.Message.Trim(),
            IsResolved = false,
            ResolutionNote = string.Empty
        });

        await dbContext.SaveChangesAsync();
        return Ok(new { message = "Đã ghi nhận thông tin liên hệ." });
    }

    private async Task<SiteSettingsResponse> GetSiteSettingsAsync()
    {
        return await dbContext.SiteSettings
            .Select(x => new SiteSettingsResponse
            {
                CompanyName = x.CompanyName,
                Tagline = x.Tagline,
                Address = x.Address,
                Hotline = x.Hotline,
                Email = x.Email,
                Website = x.Website,
                WorkingHours = x.WorkingHours,
                FooterCredentials = x.FooterCredentials
            })
            .SingleAsync();
    }

    private static readonly System.Linq.Expressions.Expression<Func<CmsPage, PageResponse>> ProjectPage = x => new PageResponse
    {
        Id = x.Id,
        Title = x.Title,
        Slug = x.Slug,
        NavigationTitle = x.NavigationTitle,
        Summary = x.Summary,
        HeroTitle = x.HeroTitle,
        HeroSummary = x.HeroSummary,
        BodyHtml = x.BodyHtml,
        MetaDescription = x.MetaDescription,
        TemplateKey = x.TemplateKey
    };

    private static readonly System.Linq.Expressions.Expression<Func<ProjectEntry, ProjectResponse>> ProjectProject = x => new ProjectResponse
    {
        Id = x.Id,
        Title = x.Title,
        Slug = x.Slug,
        Summary = x.Summary,
        BodyHtml = x.BodyHtml,
        ClientName = x.ClientName,
        Location = x.Location,
        Scale = x.Scale,
        FeaturedImageUrl = x.FeaturedImageUrl,
        Status = x.Status.ToString(),
        IsFeatured = x.IsFeatured,
        StartedOn = x.StartedOn,
        CompletedOn = x.CompletedOn
    };

    private static readonly System.Linq.Expressions.Expression<Func<ContentPost, PostResponse>> ProjectPost = x => new PostResponse
    {
        Id = x.Id,
        Title = x.Title,
        Slug = x.Slug,
        Summary = x.Summary,
        BodyHtml = x.BodyHtml,
        MetaDescription = x.MetaDescription,
        FeaturedImageUrl = x.FeaturedImageUrl,
        PostType = x.PostType.ToString(),
        CategoryName = x.Category != null ? x.Category.Name : string.Empty,
        IsFeatured = x.IsFeatured,
        PublishedAtUtc = x.PublishedAtUtc,
        JobLocation = x.JobLocation,
        ExperienceRequirement = x.ExperienceRequirement,
        SalaryRange = x.SalaryRange,
        ApplicationDeadline = x.ApplicationDeadline
    };
}
