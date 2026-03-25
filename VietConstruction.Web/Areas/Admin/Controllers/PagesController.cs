using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VietConstruction.Web.Areas.Admin.Models;
using VietConstruction.Web.Data;
using VietConstruction.Web.Data.Entities;
using VietConstruction.Web.Infrastructure;

namespace VietConstruction.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{CmsRoles.Admin},{CmsRoles.Editor}")]
public sealed class PagesController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Quản lý trang";
        ViewData["ActiveSection"] = "pages";
        return View(await dbContext.Pages.OrderBy(x => x.NavigationOrder).ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "Thêm trang";
        ViewData["ActiveSection"] = "pages";
        return View("Form", new PageEditorInput());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PageEditorInput input)
    {
        ViewData["Title"] = "Thêm trang";
        ViewData["ActiveSection"] = "pages";
        if (await dbContext.Pages.AnyAsync(x => x.Slug == input.Slug))
        {
            ModelState.AddModelError(nameof(input.Slug), "Slug đã tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            return View("Form", input);
        }

        dbContext.Pages.Add(MapPage(input, new CmsPage()));
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã tạo trang mới.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await dbContext.Pages.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật trang";
        ViewData["ActiveSection"] = "pages";
        return View("Form", MapPageInput(entity));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PageEditorInput input)
    {
        var entity = await dbContext.Pages.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật trang";
        ViewData["ActiveSection"] = "pages";
        if (await dbContext.Pages.AnyAsync(x => x.Id != id && x.Slug == input.Slug))
        {
            ModelState.AddModelError(nameof(input.Slug), "Slug đã tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            return View("Form", input);
        }

        MapPage(input, entity);
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã cập nhật trang.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await dbContext.Pages.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        if (entity.Slug == "trang-chu")
        {
            TempData["ErrorMessage"] = "Không thể xóa trang chủ mặc định.";
            return RedirectToAction(nameof(Index));
        }

        dbContext.Pages.Remove(entity);
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã xóa trang.";
        return RedirectToAction(nameof(Index));
    }

    private static CmsPage MapPage(PageEditorInput input, CmsPage entity)
    {
        entity.Title = input.Title.Trim();
        entity.Slug = input.Slug.Trim().ToLowerInvariant();
        entity.NavigationTitle = input.NavigationTitle.Trim();
        entity.MetaDescription = input.MetaDescription.Trim();
        entity.Summary = input.Summary.Trim();
        entity.HeroTitle = input.HeroTitle.Trim();
        entity.HeroSummary = input.HeroSummary.Trim();
        entity.BodyHtml = input.BodyHtml.Trim();
        entity.TemplateKey = input.TemplateKey.Trim().ToLowerInvariant();
        entity.ShowInNavigation = input.ShowInNavigation;
        entity.NavigationOrder = input.NavigationOrder;
        entity.IsPublished = input.IsPublished;
        return entity;
    }

    private static PageEditorInput MapPageInput(CmsPage entity)
    {
        return new PageEditorInput
        {
            Id = entity.Id,
            Title = entity.Title,
            Slug = entity.Slug,
            NavigationTitle = entity.NavigationTitle,
            MetaDescription = entity.MetaDescription,
            Summary = entity.Summary,
            HeroTitle = entity.HeroTitle,
            HeroSummary = entity.HeroSummary,
            BodyHtml = entity.BodyHtml,
            TemplateKey = entity.TemplateKey,
            ShowInNavigation = entity.ShowInNavigation,
            NavigationOrder = entity.NavigationOrder,
            IsPublished = entity.IsPublished
        };
    }
}
