using Microsoft.AspNetCore.Mvc;
using VietConstruction.Web.Services;

namespace VietConstruction.Web.Controllers;

public sealed class NewsController(ISiteContentService siteContentService) : Controller
{
    [HttpGet("tin-tuc")]
    public IActionResult Index([FromQuery] int page = 1) => View(siteContentService.GetNewsPage("all", page));

    [HttpGet("tin-tuc/tin-noi-bat")]
    public IActionResult TinNoiBat([FromQuery] int page = 1) => View("Index", siteContentService.GetNewsPage("featured", page));

    [HttpGet("tin-tuc/thong-bao")]
    public IActionResult ThongBao([FromQuery] int page = 1) => View("Index", siteContentService.GetNewsPage("announcements", page));

    [HttpGet("tin-tuc/tuyen-dung")]
    public IActionResult TuyenDung([FromQuery] int page = 1) => View(siteContentService.GetRecruitmentPage(page));
}
