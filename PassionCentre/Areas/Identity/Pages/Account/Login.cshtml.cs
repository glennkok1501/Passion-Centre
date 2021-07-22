using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PassionCentre.Models;
using PassionCentre.Services;

namespace PassionCentre.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ReCaptcha _captcha;
        private readonly PassionCentre.Data.PassionCentreContext _context;
        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager,
            ReCaptcha captcha,
            PassionCentre.Data.PassionCentreContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _captcha = captcha;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name ="Username")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                if (!Request.Form.ContainsKey("g-recaptcha-response")) 
                {
                    return Page();
                }
                else
                {
                    var captcha = Request.Form["g-recaptcha-response"].ToString();
                    if (!await _captcha.IsValid(captcha))
                    {
                        ModelState.AddModelError(string.Empty, "Please submit CAPTCHA");
                        return Page();
                    }
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                else if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                else if (result.IsLockedOut)
                {
                    // Login failed attempt - create an audit record
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Locked Out Account";
                    auditrecord.DateStamp = DateTime.Today.Date;
                    auditrecord.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
                    auditrecord.KeyCourseFieldID = 99999;
                    // 99999 – dummy record 

                    auditrecord.Username = Input.UserName;
                    auditrecord.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    // save the username used for the failed login
                    _context.AuditRecords.Add(auditrecord);
                    await _context.SaveChangesAsync();
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    // Login failed attempt - create an audit record
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Failed Login";
                    auditrecord.DateStamp = DateTime.Today.Date;
                    auditrecord.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
                    auditrecord.KeyCourseFieldID = 99999;
                    // 99999 – dummy record 

                    auditrecord.Username = Input.UserName;
                    auditrecord.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    // save the username used for the failed login
                    _context.AuditRecords.Add(auditrecord);
                    await _context.SaveChangesAsync();

                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
