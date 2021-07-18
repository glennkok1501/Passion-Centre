using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using PassionCentre.Models;
using Microsoft.AspNetCore.Authorization;

namespace PassionCentre.Pages.Users
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PassionCentre.Data.PassionCentreContext _context;
        public DeleteModel(UserManager<ApplicationUser> userManager, PassionCentre.Data.PassionCentreContext context)
        {
            _userManager = userManager;
            _context = context;
        }
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
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ApplicationUser = await _userManager.FindByIdAsync(id);
            var auditrecord = new AuditRecord();
            auditrecord.AuditActionType = "Delete User Record";
            auditrecord.DateStamp = DateTime.Today.Date;
            auditrecord.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
            auditrecord.KeyCourseFieldID = 99999;
            //99999 -User record
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            auditrecord.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _context.AuditRecords.Add(auditrecord);
            await _context.SaveChangesAsync();
            ApplicationUser = await _userManager.FindByIdAsync(id);
            IdentityResult userRusult = await _userManager.DeleteAsync(ApplicationUser);
            return RedirectToPage("./Index");
        }
    }
}
