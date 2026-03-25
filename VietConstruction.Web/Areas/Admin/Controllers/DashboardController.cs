using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VietConstruction.Web.Areas.Admin.Models;
using VietConstruction.Web.Data;
using VietConstruction.Web.Infrastructure;

namespace VietConstruction.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{CmsRoles.Admin},{CmsRoles.Editor}")]
public sealed class DashboardController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Tổng quan CMS";
        ViewData["ActiveSection"] = "dashboard";

        var model = new DashboardViewModel
        {
            PageCount = await dbContext.Pages.CountAsync(),
            ProjectCount = await dbContext.Projects.CountAsync(),
            PostCount = await dbContext.Posts.CountAsync(),
            ContactCount = await dbContext.ContactSubmissions.CountAsync(),
            RecentProjects = await dbContext.Projects
                .OrderByDescending(x => x.UpdatedAtUtc)
                .Take(5)
                .Select(x => new DashboardItemViewModel { Title = x.Title, Meta = $"{x.Status} | {x.Location}" })
                .ToListAsync(),
            RecentPosts = await dbContext.Posts
                .OrderByDescending(x => x.PublishedAtUtc)
                .Take(5)
                .Select(x => new DashboardItemViewModel { Title = x.Title, Meta = $"{x.PostType} | {x.PublishedAtUtc:dd/MM/yyyy}" })
                .ToListAsync(),
            RecentContacts = await dbContext.ContactSubmissions
                .OrderByDescending(x => x.CreatedAtUtc)
                .Take(5)
                .Select(x => new DashboardItemViewModel { Title = x.FullName, Meta = $"{x.PhoneNumber} | {(x.IsResolved ? "Đã xử lý" : "Chưa xử lý")}" })
                .ToListAsync()
        };

        return View(model);
    }
}
