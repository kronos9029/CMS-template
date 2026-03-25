using Microsoft.AspNetCore.Mvc;
using VietConstruction.Web.Data;
using VietConstruction.Web.Data.Entities;
using VietConstruction.Web.Models;
using VietConstruction.Web.Services;

namespace VietConstruction.Web.Controllers;

public sealed class ContactController(ISiteContentService siteContentService, ApplicationDbContext dbContext) : Controller
{
    [HttpGet("lien-he")]
    public IActionResult Index()
    {
        return View(siteContentService.GetContactPage());
    }

    [HttpPost("lien-he")]
    [ValidateAntiForgeryToken]
    public IActionResult Index(ContactFormInputModel form)
    {
        if (!ModelState.IsValid)
        {
            return View(siteContentService.GetContactPage(form));
        }

        dbContext.ContactSubmissions.Add(new ContactSubmission
        {
            FullName = form.FullName.Trim(),
            PhoneNumber = form.PhoneNumber.Trim(),
            Email = form.Email.Trim(),
            Message = form.Message.Trim(),
            IsResolved = false,
            ResolutionNote = string.Empty
        });
        dbContext.SaveChanges();

        const string submissionMessage = "Yêu cầu của Quý khách đã được ghi nhận. Bộ phận kinh doanh sẽ liên hệ trong vòng 01 ngày làm việc.";
        ModelState.Clear();
        return View(siteContentService.GetContactPage(new ContactFormInputModel(), true, submissionMessage));
    }
}
