using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PassionCentre.Data;
using PassionCentre.Models;

namespace PassionCentre.Pages.Audit
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public DeleteModel(PassionCentre.Data.PassionCentreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AuditRecord AuditRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AuditRecord = await _context.AuditRecords.FirstOrDefaultAsync(m => m.Audit_ID == id);

            if (AuditRecord == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AuditRecord = await _context.AuditRecords.FindAsync(id);

            if (AuditRecord != null)
            {
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Delete Audit Record";
                auditrecord.DateStamp = DateTime.Today.Date;
                auditrecord.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
                auditrecord.KeyCourseFieldID = AuditRecord.Audit_ID;
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;
                auditrecord.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.AuditRecords.Add(auditrecord);
                _context.AuditRecords.Remove(AuditRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
