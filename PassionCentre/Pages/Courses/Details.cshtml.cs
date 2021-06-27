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

namespace PassionCentre.Pages.Courses
{
    [Authorize(Roles = "Admin, Staff, Trainer, User")]
    public class DetailsModel : PageModel
    {
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public DetailsModel(PassionCentre.Data.PassionCentreContext context)
        {
            _context = context;
        }

        public Course Course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course = await _context.Course.FirstOrDefaultAsync(m => m.ID == id);

            if (Course == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
