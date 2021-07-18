using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PassionCentre.Data;
using PassionCentre.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace PassionCentre.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public DeleteModel(RoleManager<ApplicationRole> roleManager, PassionCentre.Data.PassionCentreContext context)
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var auditrecord = new AuditRecord();
            auditrecord.AuditActionType = "Delete ApplicationRole Record";
            auditrecord.DateStamp = DateTime.Today.Date;
            auditrecord.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
            auditrecord.KeyCourseFieldID = 9999;
            // 9999 – roles record 
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            auditrecord.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _context.AuditRecords.Add(auditrecord);
            await _context.SaveChangesAsync();

            ApplicationRole = await _roleManager.FindByIdAsync(id);
            IdentityResult roleRuslt = await _roleManager.DeleteAsync(ApplicationRole);

            return RedirectToPage("./Index");

        }
    }
}
