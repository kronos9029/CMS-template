using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VietConstruction.Web.Models;
using VietConstruction.Web.Services;

namespace VietConstruction.Web.Controllers;

public sealed class HomeController(ISiteContentService siteContentService) : Controller
{
    public IActionResult Index()
    {
        return View(siteContentService.GetHomePage());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
