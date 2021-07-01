using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PassionCentre.Data;
using PassionCentre.Models;

namespace PassionCentre.Pages.Audit
{
    public class DetailsModel : PageModel
    {
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public DetailsModel(PassionCentre.Data.PassionCentreContext context)
        {
            _context = context;
        }

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
    }
}
