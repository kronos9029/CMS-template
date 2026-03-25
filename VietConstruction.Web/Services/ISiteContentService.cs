using VietConstruction.Web.Models;

namespace VietConstruction.Web.Services;

public interface ISiteContentService
{
    SiteLayoutData GetLayoutData();

    HomePageViewModel GetHomePage();

    CorporatePageViewModel GetCompanyPage(string slug);

    ProjectListingPageViewModel GetProjectsPage(string filter);

    NewsListingPageViewModel GetNewsPage(string category);

    RecruitmentListingPageViewModel GetRecruitmentPage();

    ContactPageViewModel GetContactPage(ContactFormInputModel? form = null, bool isSubmitted = false, string? submissionMessage = null);
}
