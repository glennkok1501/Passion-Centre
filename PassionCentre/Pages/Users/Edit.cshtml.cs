using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PassionCentre.Data;
using PassionCentre.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace PassionCentre.Pages.Users
{
    [Authorize(Roles = "Admin, Staff")]
    public class EditModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public EditModel(UserManager<ApplicationUser> userManager, 
            PassionCentre.Data.PassionCentreContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser = await _userManager.FindByIdAsync(id);

            if (ApplicationUser == null)
            {
                return NotFound();
            }

            if ((_userManager.IsInRoleAsync(ApplicationUser, "Admin").Result || _userManager.IsInRoleAsync(ApplicationUser, "Staff").Result) && !User.IsInRole("Admin"))
            {
                return LocalRedirect("/Identity/Account/AccessDenied");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationUser appUser = await _userManager.FindByIdAsync(ApplicationUser.Id);

            appUser.Id = ApplicationUser.Id;
            appUser.FullName = ApplicationUser.FullName;
            appUser.BirthDate = ApplicationUser.BirthDate;

            IdentityResult userResult = await _userManager.UpdateAsync(appUser);
            if (userResult.Succeeded)
            {
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Update User Record";
                auditrecord.DateStamp = DateTime.Today.Date;
                auditrecord.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
                auditrecord.KeyCourseFieldID = 99999;
                // 99999 – dummy record 
                // Get current logged-in user
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;
                auditrecord.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }

    }
}
