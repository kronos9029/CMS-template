using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VietConstruction.Web.Areas.Admin.Models;
using VietConstruction.Web.Data.Entities;
using VietConstruction.Web.Infrastructure;

namespace VietConstruction.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = CmsRoles.Admin)]
public sealed class UsersController(UserManager<ApplicationUser> userManager) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Quản lý tài khoản";
        ViewData["ActiveSection"] = "users";

        var users = await userManager.Users.OrderBy(x => x.Email).ToListAsync();
        var rows = new List<(ApplicationUser User, string Role)>();
        foreach (var user in users)
        {
            var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? string.Empty;
            rows.Add((user, role));
        }

        return View(rows);
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "Tạo tài khoản";
        ViewData["ActiveSection"] = "users";
        return View("Form", new UserEditorInput());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserEditorInput input)
    {
        ViewData["Title"] = "Tạo tài khoản";
        ViewData["ActiveSection"] = "users";
        if (string.IsNullOrWhiteSpace(input.Password))
        {
            ModelState.AddModelError(nameof(input.Password), "Vui lòng nhập mật khẩu.");
        }

        if (!ModelState.IsValid)
        {
            return View("Form", input);
        }

        var user = new ApplicationUser
        {
            UserName = input.Email.Trim(),
            Email = input.Email.Trim(),
            FullName = input.FullName.Trim(),
            IsActive = input.IsActive,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return View("Form", input);
        }

        await userManager.AddToRoleAsync(user, input.Role);
        TempData["StatusMessage"] = "Đã tạo tài khoản mới.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật tài khoản";
        ViewData["ActiveSection"] = "users";
        var role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? CmsRoles.Editor;
        return View("Form", new UserEditorInput
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email ?? string.Empty,
            Role = role,
            IsActive = user.IsActive
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, UserEditorInput input)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        ViewData["Title"] = "Cập nhật tài khoản";
        ViewData["ActiveSection"] = "users";
        if (!ModelState.IsValid)
        {
            return View("Form", input);
        }

        user.FullName = input.FullName.Trim();
        user.UserName = input.Email.Trim();
        user.Email = input.Email.Trim();
        user.IsActive = input.IsActive;

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            AddIdentityErrors(updateResult);
            return View("Form", input);
        }

        var existingRoles = await userManager.GetRolesAsync(user);
        if (!existingRoles.Contains(input.Role))
        {
            if (existingRoles.Count > 0)
            {
                await userManager.RemoveFromRolesAsync(user, existingRoles);
            }

            await userManager.AddToRoleAsync(user, input.Role);
        }

        if (!string.IsNullOrWhiteSpace(input.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await userManager.ResetPasswordAsync(user, token, input.Password);
            if (!resetResult.Succeeded)
            {
                AddIdentityErrors(resetResult);
                return View("Form", input);
            }
        }

        TempData["StatusMessage"] = "Đã cập nhật tài khoản.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        if (user.Id == userManager.GetUserId(User))
        {
            TempData["ErrorMessage"] = "Không thể tự xóa tài khoản đang đăng nhập.";
            return RedirectToAction(nameof(Index));
        }

        await userManager.DeleteAsync(user);
        TempData["StatusMessage"] = "Đã xóa tài khoản.";
        return RedirectToAction(nameof(Index));
    }

    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
}
