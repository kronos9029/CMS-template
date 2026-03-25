using Microsoft.AspNetCore.Mvc;
using VietConstruction.Web.Services;

namespace VietConstruction.Web.Controllers;

public sealed class NewsController(ISiteContentService siteContentService) : Controller
{
    [HttpGet("tin-tuc")]
    public IActionResult Index() => View(siteContentService.GetNewsPage("all"));

    [HttpGet("tin-tuc/tin-noi-bat")]
    public IActionResult TinNoiBat() => View("Index", siteContentService.GetNewsPage("featured"));

    [HttpGet("tin-tuc/thong-bao")]
    public IActionResult ThongBao() => View("Index", siteContentService.GetNewsPage("announcements"));

    [HttpGet("tin-tuc/tuyen-dung")]
    public IActionResult TuyenDung() => View(siteContentService.GetRecruitmentPage());
}
