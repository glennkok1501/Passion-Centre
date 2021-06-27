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
    public class IndexModel : PageModel
    {
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public IndexModel(PassionCentre.Data.PassionCentreContext context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }

        public async Task OnGetAsync()
        {
            Course = await _context.Course.ToListAsync();
        }
    }
}
