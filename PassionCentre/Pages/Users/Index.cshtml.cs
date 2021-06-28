using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PassionCentre.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace PassionCentre.Pages.Users
{
    //[Authorize(Roles = "Admin, Staff")]
    public class IndexModel : PageModel
    {
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public IndexModel(PassionCentre.Data.PassionCentreContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> ApplicationUsers { get; set; }

        public async Task OnGetAsync()
        {
            ApplicationUsers = await _context.ApplicationUsers.ToListAsync();
        }
    }
}
