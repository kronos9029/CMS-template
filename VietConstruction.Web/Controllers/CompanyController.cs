using Microsoft.AspNetCore.Mvc;
using VietConstruction.Web.Services;

namespace VietConstruction.Web.Controllers;

public sealed class CompanyController(ISiteContentService siteContentService) : Controller
{
    [HttpGet("gioi-thieu")]
    public IActionResult GioiThieu() => View("Page", siteContentService.GetCompanyPage("gioi-thieu"));

    [HttpGet("co-cau-to-chuc")]
    public IActionResult CoCauToChuc() => View("Page", siteContentService.GetCompanyPage("co-cau-to-chuc"));

    [HttpGet("nang-luc-nha-thau")]
    public IActionResult NangLucNhaThau() => View("Page", siteContentService.GetCompanyPage("nang-luc-nha-thau"));

    [HttpGet("ho-so-nang-luc")]
    public IActionResult HoSoNangLuc() => View("Page", siteContentService.GetCompanyPage("ho-so-nang-luc"));

    [HttpGet("ho-so-phap-ly")]
    public IActionResult HoSoPhapLy() => View("Page", siteContentService.GetCompanyPage("ho-so-phap-ly"));

    [HttpGet("nang-luc-tai-chinh")]
    public IActionResult NangLucTaiChinh() => View("Page", siteContentService.GetCompanyPage("nang-luc-tai-chinh"));

    [HttpGet("may-moc-thiet-bi")]
    public IActionResult MayMocThietBi() => View("Page", siteContentService.GetCompanyPage("may-moc-thiet-bi"));
}
