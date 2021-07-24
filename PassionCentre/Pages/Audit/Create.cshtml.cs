using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PassionCentre.Data;
using PassionCentre.Models;

namespace PassionCentre.Pages.Audit
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public CreateModel(PassionCentre.Data.PassionCentreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AuditRecord AuditRecord { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Create audit record for creating audit
            var auditrecord = new AuditRecord();
            auditrecord.AuditActionType = "Add Audit Record";
            auditrecord.DateStamp = DateTime.Today.Date;
            auditrecord.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
            auditrecord.KeyCourseFieldID = 99999;
            //dummy record - 99999
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            auditrecord.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _context.AuditRecords.Add(auditrecord);

            ///Create audit record
            _context.AuditRecords.Add(AuditRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
