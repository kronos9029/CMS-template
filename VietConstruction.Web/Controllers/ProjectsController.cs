using Microsoft.AspNetCore.Mvc;
using VietConstruction.Web.Services;

namespace VietConstruction.Web.Controllers;

public sealed class ProjectsController(ISiteContentService siteContentService) : Controller
{
    [HttpGet("du-an")]
    public IActionResult Index() => View(siteContentService.GetProjectsPage("all"));

    [HttpGet("du-an/dang-thuc-hien")]
    public IActionResult DangThucHien() => View("Index", siteContentService.GetProjectsPage("in-progress"));

    [HttpGet("du-an/da-hoan-thanh")]
    public IActionResult DaHoanThanh() => View("Index", siteContentService.GetProjectsPage("completed"));
}
