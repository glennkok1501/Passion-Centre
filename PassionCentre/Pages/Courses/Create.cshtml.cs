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

namespace PassionCentre.Pages.Courses
{
    [Authorize(Roles = "Admin, Trainer")]
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
        public Course Course { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
