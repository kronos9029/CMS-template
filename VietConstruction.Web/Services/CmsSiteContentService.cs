using Microsoft.EntityFrameworkCore;
using VietConstruction.Web.Data;
using VietConstruction.Web.Models;

namespace VietConstruction.Web.Services;

public sealed class CmsSiteContentService(ApplicationDbContext dbContext) : ISiteContentService
{
    private static readonly HashSet<string> ReservedNavigationSlugs = new(StringComparer.OrdinalIgnoreCase)
    {
        "trang-chu",
        "gioi-thieu",
        "co-cau-to-chuc",
        "nang-luc-nha-thau",
        "ho-so-nang-luc",
        "ho-so-phap-ly",
        "nang-luc-tai-chinh",
        "may-moc-thiet-bi",
        "du-an",
        "tin-tuc",
        "lien-he"
    };

    private readonly SiteContentService inner = new(dbContext);

    public SiteLayoutData GetLayoutData()
    {
        var baseLayout = inner.GetLayoutData();
        var customNavigation = dbContext.Pages
            .AsNoTracking()
            .Where(x => x.IsPublished && x.ShowInNavigation && !ReservedNavigationSlugs.Contains(x.Slug))
            .OrderBy(x => x.NavigationOrder)
            .ThenBy(x => x.Title)
            .Select(x => new NavigationItem(
                string.IsNullOrWhiteSpace(x.NavigationTitle) ? x.Title : x.NavigationTitle,
                $"/{x.Slug}"))
            .ToList();

        if (customNavigation.Count == 0)
        {
            return baseLayout;
        }

        var navigation = baseLayout.Navigation.ToList();
        var contactIndex = navigation.FindIndex(x => string.Equals(x.Url, "/lien-he", StringComparison.OrdinalIgnoreCase));
        if (contactIndex < 0)
        {
            contactIndex = navigation.Count;
        }

        navigation.InsertRange(contactIndex, customNavigation);

        return new SiteLayoutData
        {
            Navigation = navigation,
            FooterGroups = baseLayout.FooterGroups,
            Contact = baseLayout.Contact,
            FooterCredentials = baseLayout.FooterCredentials
        };
    }

    public HomePageViewModel GetHomePage() => inner.GetHomePage();

    public CorporatePageViewModel GetCompanyPage(string slug) => inner.GetCompanyPage(slug);

    public ProjectListingPageViewModel GetProjectsPage(string filter, int page = 1) => inner.GetProjectsPage(filter, page);

    public NewsListingPageViewModel GetNewsPage(string category, int page = 1) => inner.GetNewsPage(category, page);

    public RecruitmentListingPageViewModel GetRecruitmentPage(int page = 1) => inner.GetRecruitmentPage(page);

    public ContactPageViewModel GetContactPage(ContactFormInputModel? form = null, bool isSubmitted = false, string? submissionMessage = null)
        => inner.GetContactPage(form, isSubmitted, submissionMessage);
}
