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
public sealed class CategoriesController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Danh mục bài viết";
        ViewData["ActiveSection"] = "categories";
        return View(await dbContext.Categories.OrderBy(x => x.DisplayOrder).ThenBy(x => x.Name).ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "Thêm danh mục";
        ViewData["ActiveSection"] = "categories";
        return View("Form", new CategoryEditorInput());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryEditorInput input)
    {
        ViewData["Title"] = "Thêm danh mục";
        ViewData["ActiveSection"] = "categories";
        if (await dbContext.Categories.AnyAsync(x => x.Slug == input.Slug))
        {
            ModelState.AddModelError(nameof(input.Slug), "Slug đã tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            return View("Form", input);
        }

        dbContext.Categories.Add(MapCategory(input, new PostCategory()));
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã tạo danh mục.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await dbContext.Categories.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật danh mục";
        ViewData["ActiveSection"] = "categories";
        return View("Form", MapCategoryInput(entity));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryEditorInput input)
    {
        var entity = await dbContext.Categories.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật danh mục";
        ViewData["ActiveSection"] = "categories";
        if (await dbContext.Categories.AnyAsync(x => x.Id != id && x.Slug == input.Slug))
        {
            ModelState.AddModelError(nameof(input.Slug), "Slug đã tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            return View("Form", input);
        }

        MapCategory(input, entity);
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã cập nhật danh mục.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await dbContext.Categories.Include(x => x.Posts).SingleOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return NotFound();
        }

        if (entity.Posts.Count > 0)
        {
            TempData["ErrorMessage"] = "Không thể xóa danh mục đang được bài viết sử dụng.";
            return RedirectToAction(nameof(Index));
        }

        dbContext.Categories.Remove(entity);
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã xóa danh mục.";
        return RedirectToAction(nameof(Index));
    }

    private static PostCategory MapCategory(CategoryEditorInput input, PostCategory entity)
    {
        entity.Name = input.Name.Trim();
        entity.Slug = input.Slug.Trim().ToLowerInvariant();
        entity.Description = input.Description.Trim();
        entity.PostType = input.PostType;
        entity.DisplayOrder = input.DisplayOrder;
        entity.IsVisible = input.IsVisible;
        return entity;
    }

    private static CategoryEditorInput MapCategoryInput(PostCategory entity)
    {
        return new CategoryEditorInput
        {
            Id = entity.Id,
            Name = entity.Name,
            Slug = entity.Slug,
            Description = entity.Description,
            PostType = entity.PostType,
            DisplayOrder = entity.DisplayOrder,
            IsVisible = entity.IsVisible
        };
    }
}
