using Microsoft.AspNetCore.Mvc;
using VietConstruction.Web.Services;

namespace VietConstruction.Web.Controllers;

public sealed class ProjectsController(ISiteContentService siteContentService) : Controller
{
    [HttpGet("du-an")]
    public IActionResult Index([FromQuery] int page = 1) => View(siteContentService.GetProjectsPage("all", page));

    [HttpGet("du-an/dang-thuc-hien")]
    public IActionResult DangThucHien([FromQuery] int page = 1) => View("Index", siteContentService.GetProjectsPage("in-progress", page));

    [HttpGet("du-an/da-hoan-thanh")]
    public IActionResult DaHoanThanh([FromQuery] int page = 1) => View("Index", siteContentService.GetProjectsPage("completed", page));
}
