using VietConstruction.Web.Models;

namespace VietConstruction.Web.Services;

public interface ISiteContentService
{
    SiteLayoutData GetLayoutData();

    HomePageViewModel GetHomePage();

    CorporatePageViewModel GetCompanyPage(string slug);

    ProjectListingPageViewModel GetProjectsPage(string filter, int page = 1);

    NewsListingPageViewModel GetNewsPage(string category, int page = 1);

    RecruitmentListingPageViewModel GetRecruitmentPage(int page = 1);

    ContactPageViewModel GetContactPage(ContactFormInputModel? form = null, bool isSubmitted = false, string? submissionMessage = null);
}
