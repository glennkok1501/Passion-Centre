using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using PassionCentre.Models;
using Microsoft.AspNetCore.Authorization;

namespace PassionCentre.Pages.Users
{
    [Authorize(Roles = "Admin, Staff")]
    public class EndLockOutModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public EndLockOutModel(UserManager<ApplicationUser> userManager)
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
            var result = await _userManager.SetLockoutEndDateAsync(ApplicationUser, null);
            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(ApplicationUser);
            }
            return Page();
        }
        
    }
}
