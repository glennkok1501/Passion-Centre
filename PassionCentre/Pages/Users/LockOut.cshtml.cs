using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using PassionCentre.Models;

namespace PassionCentre.Pages.Users
{
    public class LockOutModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public LockOutModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
            await _userManager.SetLockoutEndDateAsync(ApplicationUser, DateTimeOffset.MaxValue);
            return Page();
        }

    }
}
