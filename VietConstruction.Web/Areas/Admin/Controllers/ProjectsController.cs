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
public sealed class ProjectsController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Quản lý dự án";
        ViewData["ActiveSection"] = "projects";
        return View(await dbContext.Projects.OrderBy(x => x.DisplayOrder).ThenByDescending(x => x.UpdatedAtUtc).ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "Thêm dự án";
        ViewData["ActiveSection"] = "projects";
        return View("Form", new ProjectEditorInput());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectEditorInput input)
    {
        ViewData["Title"] = "Thêm dự án";
        ViewData["ActiveSection"] = "projects";
        if (await dbContext.Projects.AnyAsync(x => x.Slug == input.Slug))
        {
            ModelState.AddModelError(nameof(input.Slug), "Slug đã tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            return View("Form", input);
        }

        dbContext.Projects.Add(MapProject(input, new ProjectEntry()));
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã tạo dự án mới.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await dbContext.Projects.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật dự án";
        ViewData["ActiveSection"] = "projects";
        return View("Form", MapProjectInput(entity));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProjectEditorInput input)
    {
        var entity = await dbContext.Projects.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật dự án";
        ViewData["ActiveSection"] = "projects";
        if (await dbContext.Projects.AnyAsync(x => x.Id != id && x.Slug == input.Slug))
        {
            ModelState.AddModelError(nameof(input.Slug), "Slug đã tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            return View("Form", input);
        }

        MapProject(input, entity);
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã cập nhật dự án.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await dbContext.Projects.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        dbContext.Projects.Remove(entity);
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã xóa dự án.";
        return RedirectToAction(nameof(Index));
    }

    private static ProjectEntry MapProject(ProjectEditorInput input, ProjectEntry entity)
    {
        entity.Title = input.Title.Trim();
        entity.Slug = input.Slug.Trim().ToLowerInvariant();
        entity.Summary = input.Summary.Trim();
        entity.BodyHtml = input.BodyHtml.Trim();
        entity.ClientName = input.ClientName.Trim();
        entity.Location = input.Location.Trim();
        entity.Scale = input.Scale.Trim();
        entity.FeaturedImageUrl = input.FeaturedImageUrl.Trim();
        entity.IsFeatured = input.IsFeatured;
        entity.IsPublished = input.IsPublished;
        entity.Status = input.Status;
        entity.DisplayOrder = input.DisplayOrder;
        entity.StartedOn = input.StartedOn;
        entity.CompletedOn = input.CompletedOn;
        return entity;
    }

    private static ProjectEditorInput MapProjectInput(ProjectEntry entity)
    {
        return new ProjectEditorInput
        {
            Id = entity.Id,
            Title = entity.Title,
            Slug = entity.Slug,
            Summary = entity.Summary,
            BodyHtml = entity.BodyHtml,
            ClientName = entity.ClientName,
            Location = entity.Location,
            Scale = entity.Scale,
            FeaturedImageUrl = entity.FeaturedImageUrl,
            IsFeatured = entity.IsFeatured,
            IsPublished = entity.IsPublished,
            Status = entity.Status,
            DisplayOrder = entity.DisplayOrder,
            StartedOn = entity.StartedOn,
            CompletedOn = entity.CompletedOn
        };
    }
}
