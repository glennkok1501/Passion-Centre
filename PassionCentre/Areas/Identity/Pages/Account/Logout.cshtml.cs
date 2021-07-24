using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PassionCentre.Models;

namespace PassionCentre.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly PassionCentre.Data.PassionCentreContext _context;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, PassionCentre.Data.PassionCentreContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = User.Identity.Name;
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            // Login failed attempt - create an audit record
            var auditrecord = new AuditRecord();
            auditrecord.AuditActionType = "User log out";
            auditrecord.DateStamp = DateTime.Today.Date;
            auditrecord.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
            auditrecord.KeyCourseFieldID = 99999;
            // 99999 – dummy record 

            auditrecord.Username = user;
            auditrecord.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            // save the username used for the failed login
            _context.AuditRecords.Add(auditrecord);
            await _context.SaveChangesAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
