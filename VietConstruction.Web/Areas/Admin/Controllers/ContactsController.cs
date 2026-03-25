using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VietConstruction.Web.Areas.Admin.Models;
using VietConstruction.Web.Data;
using VietConstruction.Web.Infrastructure;

namespace VietConstruction.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{CmsRoles.Admin},{CmsRoles.Editor}")]
public sealed class ContactsController(ApplicationDbContext dbContext) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Liên hệ nhận được";
        ViewData["ActiveSection"] = "contacts";
        return View(await dbContext.ContactSubmissions.OrderByDescending(x => x.CreatedAtUtc).ToListAsync());
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await dbContext.ContactSubmissions.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật trạng thái liên hệ";
        ViewData["ActiveSection"] = "contacts";
        return View(new ContactResolutionInput
        {
            Id = entity.Id,
            IsResolved = entity.IsResolved,
            ResolutionNote = entity.ResolutionNote
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ContactResolutionInput input)
    {
        var entity = await dbContext.ContactSubmissions.FindAsync(id);
        if (entity is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật trạng thái liên hệ";
        ViewData["ActiveSection"] = "contacts";
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        entity.IsResolved = input.IsResolved;
        entity.ResolutionNote = input.ResolutionNote.Trim();
        await dbContext.SaveChangesAsync();
        TempData["StatusMessage"] = "Đã cập nhật trạng thái liên hệ.";
        return RedirectToAction(nameof(Index));
    }
}
