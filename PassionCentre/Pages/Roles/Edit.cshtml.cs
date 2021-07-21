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

namespace PassionCentre.Pages.Roles
{
    [Authorize(Roles = "Admin, Staff")] //I don't think Staff should be able to edit Roles - Glenn
    public class EditModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public EditModel(RoleManager<ApplicationRole> roleManager, PassionCentre.Data.PassionCentreContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public ApplicationRole ApplicationRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationRole = await _roleManager.FindByIdAsync(id);

            if (ApplicationRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationRole appRole = await _roleManager.FindByIdAsync(ApplicationRole.Id);

            appRole.Id = ApplicationRole.Id;
            appRole.Name = ApplicationRole.Name;
            appRole.Description = ApplicationRole.Description;

            IdentityResult roleRuslt = await _roleManager.UpdateAsync(appRole);

            if (roleRuslt.Succeeded)
            {
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Edit ApplicationRole Record";
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
                return RedirectToPage("./Index");

            }
            return RedirectToPage("./Index");
        }

    }
}