using Microsoft.EntityFrameworkCore;
using VietConstruction.Web.Data;
using VietConstruction.Web.Data.Entities;
using VietConstruction.Web.Models;

namespace VietConstruction.Web.Services;

public sealed class SiteContentService(ApplicationDbContext dbContext) : ISiteContentService
{
    private static readonly FooterLinkGroup[] FooterGroups =
    [
        new("Doanh nghiệp", [new ActionLink("Giới thiệu", "/gioi-thieu"), new ActionLink("Cơ cấu tổ chức", "/co-cau-to-chuc"), new ActionLink("Liên hệ", "/lien-he")]),
        new("Năng lực", [new ActionLink("Năng lực nhà thầu", "/nang-luc-nha-thau"), new ActionLink("Hồ sơ pháp lý", "/ho-so-phap-ly"), new ActionLink("Năng lực tài chính", "/nang-luc-tai-chinh")]),
        new("Truyền thông", [new ActionLink("Dự án", "/du-an"), new ActionLink("Tin tức", "/tin-tuc"), new ActionLink("Tuyển dụng", "/tin-tuc/tuyen-dung")])
    ];

    private static readonly string[] FooterCredentials =
    [
        "ISO 9001:2015",
        "ISO 14001:2015",
        "ISO 45001:2018",
        "Năng lực triển khai toàn quốc"
    ];

    public SiteLayoutData GetLayoutData()
    {
        var site = GetSiteSetting();
        var pages = dbContext.Pages
            .AsNoTracking()
            .Where(x => x.IsPublished)
            .ToList();

        var pageMap = pages.ToDictionary(x => x.Slug, StringComparer.OrdinalIgnoreCase);

        return new SiteLayoutData
        {
            Navigation =
            [
                CreateNavigation(pageMap, "trang-chu", "Trang chủ", "/"),
                CreateNavigation(pageMap, "gioi-thieu", "Giới thiệu", "/gioi-thieu"),
                CreateNavigation(pageMap, "co-cau-to-chuc", "Cơ cấu tổ chức", "/co-cau-to-chuc"),
                new NavigationItem(
                    GetPageTitle(pageMap, "nang-luc-nha-thau", "Năng lực nhà thầu"),
                    "/nang-luc-nha-thau",
                    [
                        CreateNavigation(pageMap, "ho-so-nang-luc", "Hồ sơ năng lực", "/ho-so-nang-luc"),
                        CreateNavigation(pageMap, "ho-so-phap-ly", "Hồ sơ pháp lý", "/ho-so-phap-ly"),
                        CreateNavigation(pageMap, "nang-luc-tai-chinh", "Năng lực tài chính", "/nang-luc-tai-chinh"),
                        CreateNavigation(pageMap, "may-moc-thiet-bi", "Máy móc thiết bị", "/may-moc-thiet-bi")
                    ]),
                new NavigationItem(
                    GetPageTitle(pageMap, "du-an", "Dự án"),
                    "/du-an",
                    [
                        new NavigationItem("Dự án đang thực hiện", "/du-an/dang-thuc-hien"),
                        new NavigationItem("Dự án đã hoàn thành", "/du-an/da-hoan-thanh")
                    ]),
                new NavigationItem(
                    GetPageTitle(pageMap, "tin-tuc", "Tin tức"),
                    "/tin-tuc",
                    [
                        new NavigationItem("Tin nổi bật", "/tin-tuc/tin-noi-bat"),
                        new NavigationItem("Thông báo", "/tin-tuc/thong-bao"),
                        new NavigationItem("Tuyển dụng", "/tin-tuc/tuyen-dung")
                    ]),
                CreateNavigation(pageMap, "lien-he", "Liên hệ", "/lien-he")
            ],
            FooterGroups = FooterGroups,
            Contact = new CompanyContact(
                site.CompanyName,
                site.Tagline,
                site.Address,
                site.Hotline,
                site.Email,
                site.Website,
                site.WorkingHours),
            FooterCredentials = FooterCredentials
        };
    }

    public HomePageViewModel GetHomePage()
    {
        var site = GetSiteSetting();
        var homePage = dbContext.Pages
            .AsNoTracking()
            .SingleOrDefault(x => x.Slug == "trang-chu" && x.IsPublished);

        var featuredProjects = dbContext.Projects
            .AsNoTracking()
            .Where(x => x.IsPublished && x.IsFeatured)
            .OrderBy(x => x.DisplayOrder)
            .Take(3)
            .ToList()
            .Select(MapProject)
            .ToArray();

        var latestNews = dbContext.Posts
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.IsPublished && x.PostType != PostType.Recruitment)
            .OrderByDescending(x => x.PublishedAtUtc)
            .Take(3)
            .ToList()
            .Select(MapArticle)
            .ToArray();

        var jobs = dbContext.Posts
            .AsNoTracking()
            .Where(x => x.IsPublished && x.PostType == PostType.Recruitment)
            .OrderByDescending(x => x.PublishedAtUtc)
            .Take(2)
            .ToList()
            .Select(MapJob)
            .ToArray();

        return new HomePageViewModel
        {
            Title = homePage?.Title ?? "Trang chủ",
            MetaDescription = homePage?.MetaDescription ?? site.HeroSummary,
            HeroTitle = homePage?.HeroTitle ?? site.HeroTitle,
            HeroSummary = homePage?.HeroSummary ?? site.HeroSummary,
            HeroActions =
            [
                new ActionLink("Hồ sơ năng lực", "/ho-so-nang-luc", true),
                new ActionLink("Liên hệ tư vấn", "/lien-he")
            ],
            HeroMetrics =
            [
                new MetricItem("18+", "Năm kinh nghiệm"),
                new MetricItem(dbContext.Projects.Count().ToString(), "Dự án đã cập nhật"),
                new MetricItem(dbContext.Posts.Count(x => x.IsPublished).ToString(), "Bài viết xuất bản"),
                new MetricItem("02", "Vai trò CMS")
            ],
            IntroTitle = "Đối tác xây dựng đáng tin cậy cho chủ đầu tư và doanh nghiệp",
            IntroSummary = homePage?.Summary ?? site.HeroSummary,
            IntroHighlights =
            [
                "Quản trị dự án theo KPI tiến độ, chi phí và an toàn ở từng giai đoạn.",
                "Hệ thống pháp lý, tài chính, máy móc và nhân sự sẵn sàng triển khai đồng thời nhiều công trình.",
                "Toàn bộ nội dung website được cập nhật tập trung từ CMS .NET duy nhất."
            ],
            CapabilityHighlights =
            [
                new ShowcaseCardViewModel
                {
                    Title = "Năng lực nhà thầu",
                    Summary = "Thi công kết cấu, hoàn thiện, MEP và hạ tầng với mô hình điều hành tập trung và hồ sơ chất lượng rõ ràng.",
                    Tag = "Thi công tổng thầu",
                    LinkUrl = "/nang-luc-nha-thau",
                    LinkLabel = "Xem năng lực"
                },
                new ShowcaseCardViewModel
                {
                    Title = "Hồ sơ pháp lý",
                    Summary = "Giấy phép, chứng chỉ năng lực, chứng nhận hệ thống quản lý và hồ sơ tuân thủ được trình bày minh bạch.",
                    Tag = "Pháp lý minh bạch",
                    LinkUrl = "/ho-so-phap-ly",
                    LinkLabel = "Xem hồ sơ"
                },
                new ShowcaseCardViewModel
                {
                    Title = "Năng lực tài chính",
                    Summary = "Năng lực tài chính ổn định, hạn mức tín dụng và khả năng huy động nguồn lực cho các công trình quy mô lớn.",
                    Tag = "Tài chính",
                    LinkUrl = "/nang-luc-tai-chinh",
                    LinkLabel = "Xem chi tiết"
                },
                new ShowcaseCardViewModel
                {
                    Title = "Máy móc thiết bị",
                    Summary = "Danh mục thiết bị thi công chủ lực phục vụ dân dụng, công nghiệp và hạ tầng kỹ thuật.",
                    Tag = "Thiết bị hiện trường",
                    LinkUrl = "/may-moc-thiet-bi",
                    LinkLabel = "Xem danh mục"
                }
            ],
            FeaturedProjects = featuredProjects,
            LatestNews = latestNews,
            RecruitmentPreview = jobs
        };
    }

    public CorporatePageViewModel GetCompanyPage(string slug)
    {
        var page = dbContext.Pages
            .AsNoTracking()
            .SingleOrDefault(x => x.Slug == slug && x.IsPublished)
            ?? throw new KeyNotFoundException($"Không tìm thấy trang với mã '{slug}'.");

        return new CorporatePageViewModel
        {
            Title = page.Title,
            MetaDescription = page.MetaDescription,
            Eyebrow = page.NavigationTitle,
            HeroTitle = page.HeroTitle,
            HeroSummary = page.HeroSummary,
            HeroActions = GetDefaultPageActions(slug),
            Sections =
            [
                new ContentSectionViewModel
                {
                    AnchorId = "noi-dung",
                    Title = page.Title,
                    Summary = page.Summary,
                    HtmlContent = page.BodyHtml
                }
            ],
            Highlights = GetPageHighlights(slug)
        };
    }

    public ProjectListingPageViewModel GetProjectsPage(string filter)
    {
        var query = dbContext.Projects.AsNoTracking().Where(x => x.IsPublished);
        if (string.Equals(filter, "in-progress", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(x => x.Status == ProjectStatus.Ongoing);
        }
        else if (string.Equals(filter, "completed", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(x => x.Status == ProjectStatus.Completed);
        }

        return new ProjectListingPageViewModel
        {
            Title = filter switch
            {
                "in-progress" => "Dự án đang thực hiện",
                "completed" => "Dự án đã hoàn thành",
                _ => "Dự án"
            },
            MetaDescription = "Danh mục dự án xây dựng tiêu biểu của doanh nghiệp, bao gồm công trình đang triển khai và đã bàn giao.",
            Eyebrow = "Danh mục công trình",
            HeroTitle = "Dự án tiêu biểu theo từng giai đoạn triển khai",
            HeroSummary = "Danh mục công trình được tổ chức rõ ràng để giới thiệu năng lực nhà thầu, mức độ đa dạng loại hình dự án và kinh nghiệm triển khai thực tế.",
            CurrentFilter = filter,
            HeroActions =
            [
                new ActionLink("Dự án đang thực hiện", "/du-an/dang-thuc-hien", filter == "in-progress"),
                new ActionLink("Dự án đã hoàn thành", "/du-an/da-hoan-thanh", filter == "completed")
            ],
            HeroMetrics =
            [
                new MetricItem(dbContext.Projects.Count().ToString(), "Tổng dự án"),
                new MetricItem(dbContext.Projects.Count(x => x.Status == ProjectStatus.Ongoing).ToString(), "Đang thực hiện"),
                new MetricItem(dbContext.Projects.Count(x => x.Status == ProjectStatus.Completed).ToString(), "Đã hoàn thành"),
                new MetricItem(dbContext.Projects.Select(x => x.Location).Distinct().Count().ToString(), "Địa bàn")
            ],
            Projects = query
                .OrderBy(x => x.DisplayOrder)
                .ToList()
                .Select(MapProject)
                .ToArray()
        };
    }

    public NewsListingPageViewModel GetNewsPage(string category)
    {
        var query = dbContext.Posts
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.IsPublished && x.PostType != PostType.Recruitment);

        if (string.Equals(category, "featured", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(x => x.Category != null && x.Category.Slug == "tin-noi-bat");
        }
        else if (string.Equals(category, "announcements", StringComparison.OrdinalIgnoreCase))
        {
            query = query.Where(x => x.Category != null && x.Category.Slug == "thong-bao");
        }

        return new NewsListingPageViewModel
        {
            Title = category switch
            {
                "featured" => "Tin nổi bật",
                "announcements" => "Thông báo",
                _ => "Tin tức"
            },
            MetaDescription = "Cập nhật tin nổi bật, thông báo doanh nghiệp và các hoạt động tiêu biểu của đơn vị.",
            Eyebrow = "Cập nhật doanh nghiệp",
            HeroTitle = "Tin tức, thông báo và hoạt động nổi bật",
            HeroSummary = "Chuyên mục tin tức được tổ chức đơn giản để bộ phận nội dung có thể đăng tải nhanh thông tin công trình, sự kiện nội bộ và thông báo doanh nghiệp.",
            CurrentCategory = category,
            HeroActions =
            [
                new ActionLink("Tin nổi bật", "/tin-tuc/tin-noi-bat", category == "featured"),
                new ActionLink("Thông báo", "/tin-tuc/thong-bao", category == "announcements"),
                new ActionLink("Tuyển dụng", "/tin-tuc/tuyen-dung")
            ],
            Articles = query
                .OrderByDescending(x => x.PublishedAtUtc)
                .ToList()
                .Select(MapArticle)
                .ToArray()
        };
    }

    public RecruitmentListingPageViewModel GetRecruitmentPage()
    {
        return new RecruitmentListingPageViewModel
        {
            Title = "Tuyển dụng",
            MetaDescription = "Thông tin tuyển dụng dành cho kỹ sư, cán bộ quản lý dự án và nhân sự hỗ trợ tại doanh nghiệp.",
            Eyebrow = "Cơ hội nghề nghiệp",
            HeroTitle = "Tuyển dụng nhân sự đồng hành cùng các dự án trọng điểm",
            HeroSummary = "Trang tuyển dụng tập trung vào nhu cầu nhân sự hiện trường và khối văn phòng, với mẫu nội dung ngắn gọn, dễ đăng và dễ cập nhật trên CMS.",
            HeroActions =
            [
                new ActionLink("Gửi hồ sơ qua email", "mailto:tuyendung@vietthanhcong.vn", true),
                new ActionLink("Liên hệ phòng nhân sự", "/lien-he")
            ],
            Jobs = dbContext.Posts
                .AsNoTracking()
                .Where(x => x.IsPublished && x.PostType == PostType.Recruitment)
                .OrderByDescending(x => x.PublishedAtUtc)
                .ToList()
                .Select(MapJob)
                .ToArray()
        };
    }

    public ContactPageViewModel GetContactPage(ContactFormInputModel? form = null, bool isSubmitted = false, string? submissionMessage = null)
    {
        var site = GetSiteSetting();

        return new ContactPageViewModel
        {
            Title = "Liên hệ",
            MetaDescription = "Thông tin liên hệ doanh nghiệp, văn phòng, hotline và biểu mẫu tiếp nhận yêu cầu tư vấn.",
            Eyebrow = "Kết nối nhanh",
            HeroTitle = "Liên hệ tư vấn và trao đổi cơ hội hợp tác",
            HeroSummary = "Bộ phận kinh doanh, đấu thầu và nhân sự tiếp nhận yêu cầu qua biểu mẫu hoặc liên hệ trực tiếp trong giờ hành chính.",
            Offices =
            [
                new OfficeCardViewModel
                {
                    Name = "Văn phòng trụ sở",
                    Address = site.Address,
                    Hotline = site.Hotline,
                    Email = site.Email,
                    WorkingHours = site.WorkingHours
                },
                new OfficeCardViewModel
                {
                    Name = "Bộ phận triển khai dự án",
                    Address = "Khối quản lý công trường và điều phối tiến độ theo từng dự án đang hoạt động",
                    Hotline = site.Hotline,
                    Email = "duan@vietthanhcong.vn",
                    WorkingHours = site.WorkingHours
                }
            ],
            SupportItems =
            [
                new KeyValueItem("Hotline dự án", site.Hotline),
                new KeyValueItem("Phòng đấu thầu", "tender@vietthanhcong.vn"),
                new KeyValueItem("Phòng nhân sự", "tuyendung@vietthanhcong.vn"),
                new KeyValueItem("Website", site.Website)
            ],
            Form = form ?? new ContactFormInputModel(),
            IsSubmitted = isSubmitted,
            SubmissionMessage = submissionMessage
        };
    }

    private SiteSetting GetSiteSetting()
    {
        return dbContext.SiteSettings.AsNoTracking().OrderBy(x => x.Id).First();
    }

    private static NavigationItem CreateNavigation(IReadOnlyDictionary<string, CmsPage> pageMap, string slug, string fallbackLabel, string fallbackUrl)
    {
        return new NavigationItem(GetPageTitle(pageMap, slug, fallbackLabel), fallbackUrl);
    }

    private static string GetPageTitle(IReadOnlyDictionary<string, CmsPage> pageMap, string slug, string fallbackLabel)
    {
        return pageMap.TryGetValue(slug, out var page) ? page.NavigationTitle : fallbackLabel;
    }

    private static ActionLink[] GetDefaultPageActions(string slug)
    {
        return slug switch
        {
            "gioi-thieu" => [new ActionLink("Xem hồ sơ năng lực", "/ho-so-nang-luc", true), new ActionLink("Dự án tiêu biểu", "/du-an")],
            "nang-luc-nha-thau" => [new ActionLink("Dự án đang thực hiện", "/du-an/dang-thuc-hien", true), new ActionLink("Liên hệ tư vấn", "/lien-he")],
            "ho-so-phap-ly" => [new ActionLink("Liên hệ hợp tác", "/lien-he", true)],
            "nang-luc-tai-chinh" => [new ActionLink("Hồ sơ pháp lý", "/ho-so-phap-ly", true), new ActionLink("Liên hệ tư vấn", "/lien-he")],
            "may-moc-thiet-bi" => [new ActionLink("Năng lực nhà thầu", "/nang-luc-nha-thau", true)],
            _ => [new ActionLink("Liên hệ tư vấn", "/lien-he", true)]
        };
    }

    private static ShowcaseCardViewModel[] GetPageHighlights(string slug)
    {
        return slug switch
        {
            "gioi-thieu" =>
            [
                new ShowcaseCardViewModel { Title = "Định hướng phát triển", Summary = "Tập trung thị trường công nghiệp, logistics, y tế và cao tầng tại các đô thị trọng điểm.", Tag = "Chiến lược" },
                new ShowcaseCardViewModel { Title = "Cam kết chất lượng", Summary = "Áp dụng hệ thống quản lý chất lượng và kiểm soát hồ sơ hoàn công theo chuẩn thống nhất.", Tag = "Chất lượng" },
                new ShowcaseCardViewModel { Title = "An toàn lao động", Summary = "Duy trì kiểm tra an toàn theo ca, theo tuần và theo mốc thi công quan trọng.", Tag = "An toàn" }
            ],
            "co-cau-to-chuc" =>
            [
                new ShowcaseCardViewModel
                {
                    Title = "Khối điều hành",
                    Summary = "Điều phối chiến lược, tài chính, tiến độ và kiểm soát vận hành toàn hệ thống.",
                    Tag = "Quản trị"
                },
                new ShowcaseCardViewModel
                {
                    Title = "Khối kỹ thuật",
                    Summary = "Phụ trách hồ sơ thi công, biện pháp, QA/QC và shopdrawing cho từng dự án.",
                    Tag = "Kỹ thuật"
                }
            ],
            "may-moc-thiet-bi" =>
            [
                new ShowcaseCardViewModel { Title = "Thiết bị nâng", Summary = "Phục vụ công trình cao tầng, kho xưởng và công trình có yêu cầu bốc dỡ vật tư lớn.", Tag = "Cẩu tháp và xe cẩu", ImageUrl = "/images/project-office.svg" },
                new ShowcaseCardViewModel { Title = "Thiết bị nền móng", Summary = "Đáp ứng thi công móng cọc, san nền, hạ tầng và xử lý nền công trình.", Tag = "Máy đào và máy ủi", ImageUrl = "/images/project-bridge.svg" },
                new ShowcaseCardViewModel { Title = "Thiết bị hoàn thiện", Summary = "Hệ thống giàn giáo, coppha và thiết bị hỗ trợ hoàn thiện được chuẩn hóa theo từng nhóm dự án.", Tag = "Hoàn thiện", ImageUrl = "/images/project-residential.svg" }
            ],
            _ => []
        };
    }

    private static ProjectCardViewModel MapProject(ProjectEntry project)
    {
        return new ProjectCardViewModel
        {
            Title = project.Title,
            Status = project.Status == ProjectStatus.Ongoing ? "Đang thực hiện" : "Đã hoàn thành",
            Location = project.Location,
            Client = project.ClientName,
            Scale = project.Scale,
            Summary = project.Summary,
            ImageUrl = project.FeaturedImageUrl
        };
    }

    private static ArticleCardViewModel MapArticle(ContentPost post)
    {
        return new ArticleCardViewModel
        {
            Title = post.Title,
            Category = post.Category?.Name ?? GetCategoryLabel(post.PostType),
            PublishedOn = DateOnly.FromDateTime(post.PublishedAtUtc),
            Summary = post.Summary,
            ImageUrl = post.FeaturedImageUrl
        };
    }

    private static JobCardViewModel MapJob(ContentPost post)
    {
        return new JobCardViewModel
        {
            Title = post.Title,
            Location = string.IsNullOrWhiteSpace(post.JobLocation) ? "Theo dự án" : post.JobLocation,
            Experience = string.IsNullOrWhiteSpace(post.ExperienceRequirement) ? "Theo yêu cầu công việc" : post.ExperienceRequirement,
            Salary = string.IsNullOrWhiteSpace(post.SalaryRange) ? "Thỏa thuận" : post.SalaryRange,
            Deadline = post.ApplicationDeadline ?? DateOnly.FromDateTime(post.PublishedAtUtc.AddDays(30)),
            Summary = post.Summary
        };
    }

    private static string GetCategoryLabel(PostType postType)
    {
        return postType switch
        {
            PostType.News => "Tin nổi bật",
            PostType.Announcement => "Thông báo",
            PostType.Recruitment => "Tuyển dụng",
            _ => "Bài viết"
        };
    }
}
