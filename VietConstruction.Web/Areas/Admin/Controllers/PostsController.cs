using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VietConstruction.Web.Areas.Admin.Models;
using VietConstruction.Web.Data;
using VietConstruction.Web.Data.Entities;
using VietConstruction.Web.Infrastructure;

namespace VietConstruction.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{CmsRoles.Admin},{CmsRoles.Editor}")]
public sealed class PostsController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Quản lý bài viết";
        ViewData["ActiveSection"] = "posts";
        return View(await dbContext.Posts.Include(x => x.Category).OrderByDescending(x => x.PublishedAtUtc).ToListAsync());
    }

    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Thêm bài viết";
        ViewData["ActiveSection"] = "posts";
        await PopulateCategoryOptionsAsync();
        return View("Form", new PostEditorInput { PublishedAtUtc = DateTime.UtcNow });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PostEditorInput input)
    {
        ViewData["Title"] = "Thêm bài viết";
        ViewData["ActiveSection"] = "posts";
        await ValidatePostAsync(input);
        if (!ModelState.IsValid)
        {
            await PopulateCategoryOptionsAsync();
            return View("Form", input);
        }

        dbContext.Posts.Add(MapPost(input, new ContentPost()));
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã tạo bài viết.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await dbContext.Posts.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật bài viết";
        ViewData["ActiveSection"] = "posts";
        await PopulateCategoryOptionsAsync();
        return View("Form", MapPostInput(entity));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PostEditorInput input)
    {
        var entity = await dbContext.Posts.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật bài viết";
        ViewData["ActiveSection"] = "posts";
        await ValidatePostAsync(input, id);
        if (!ModelState.IsValid)
        {
            await PopulateCategoryOptionsAsync();
            return View("Form", input);
        }

        MapPost(input, entity);
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã cập nhật bài viết.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await dbContext.Posts.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        dbContext.Posts.Remove(entity);
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã xóa bài viết.";
        return RedirectToAction(nameof(Index));
    }

    private async Task ValidatePostAsync(PostEditorInput input, int? ignoreId = null)
    {
        if (await dbContext.Posts.AnyAsync(x => x.Id != ignoreId && x.Slug == input.Slug))
        {
            ModelState.AddModelError(nameof(input.Slug), "Slug đã tồn tại.");
        }

        var category = await dbContext.Categories.FindAsync(input.CategoryId);
        if (category is null)
        {
            ModelState.AddModelError(nameof(input.CategoryId), "Danh mục không hợp lệ.");
        }
        else if (category.PostType != input.PostType)
        {
            ModelState.AddModelError(nameof(input.CategoryId), "Danh mục phải cùng loại với bài viết.");
        }
    }

    private async Task PopulateCategoryOptionsAsync()
    {
        ViewBag.CategoryOptions = await dbContext.Categories
            .OrderBy(x => x.DisplayOrder)
            .Select(x => new SelectListItem($"{x.Name} ({x.PostType})", x.Id.ToString()))
            .ToListAsync();
    }

    private static ContentPost MapPost(PostEditorInput input, ContentPost entity)
    {
        entity.Title = input.Title.Trim();
        entity.Slug = input.Slug.Trim().ToLowerInvariant();
        entity.Summary = input.Summary.Trim();
        entity.BodyHtml = input.BodyHtml.Trim();
        entity.MetaDescription = input.MetaDescription.Trim();
        entity.FeaturedImageUrl = input.FeaturedImageUrl.Trim();
        entity.PostType = input.PostType;
        entity.CategoryId = input.CategoryId;
        entity.IsFeatured = input.IsFeatured;
        entity.IsPublished = input.IsPublished;
        entity.PublishedAtUtc = DateTime.SpecifyKind(input.PublishedAtUtc, DateTimeKind.Utc);
        entity.JobLocation = input.JobLocation.Trim();
        entity.ExperienceRequirement = input.ExperienceRequirement.Trim();
        entity.SalaryRange = input.SalaryRange.Trim();
        entity.ApplicationDeadline = input.ApplicationDeadline;
        return entity;
    }

    private static PostEditorInput MapPostInput(ContentPost entity)
    {
        return new PostEditorInput
        {
            Id = entity.Id,
            Title = entity.Title,
            Slug = entity.Slug,
            Summary = entity.Summary,
            BodyHtml = entity.BodyHtml,
            MetaDescription = entity.MetaDescription,
            FeaturedImageUrl = entity.FeaturedImageUrl,
            PostType = entity.PostType,
            CategoryId = entity.CategoryId,
            IsFeatured = entity.IsFeatured,
            IsPublished = entity.IsPublished,
            PublishedAtUtc = entity.PublishedAtUtc,
            JobLocation = entity.JobLocation,
            ExperienceRequirement = entity.ExperienceRequirement,
            SalaryRange = entity.SalaryRange,
            ApplicationDeadline = entity.ApplicationDeadline
        };
    }
}
