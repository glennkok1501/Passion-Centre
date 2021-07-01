using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PassionCentre.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PassionCentre.Pages.Courses
{
    [Authorize(Roles = "Admin, Staff")]
    public class ManageModel : PageModel
    {
        private readonly PassionCentre.Data.PassionCentreContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ManageModel(PassionCentre.Data.PassionCentreContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public SelectList RolesSelectList;
        public SelectList StaffRolesSelectList;
        public SelectList UsersSelectList;
        public SelectList StaffUsersSelectList;

        public List<string> staffrolePermit = new List<string>()
            {
                "Trainer",
            };

        public List<string> staffuserPermit = new List<string>()
            {
                
            };

        public string selectedrolename { set; get; }
        public string selectedusername { set; get; }
        public string delrolename { set; get; }
        public string delusername { set; get; }

        public int usercountinrole { set; get; }
        public IList<ApplicationRole> Listroles { get; set; }

        public string ListUsersInRole(string rolename)
        {
            // List users  based on specified role as parameter
            string strListUsersInRole = "";
            string roleid = _roleManager.Roles.SingleOrDefault(u => u.Name == rolename).Id;

            // No. of users for each specified role
            var count = _context.UserRoles.Where(u => u.RoleId == roleid).Count();
            usercountinrole = count;

            //List of users for each specified role
            var listusers = _context.UserRoles.Where(u => u.RoleId == roleid);

            foreach (var oParam in listusers)
            {    
                var userobj = _context.Users.SingleOrDefault(s => s.Id == oParam.UserId);
                strListUsersInRole += "[" + userobj.UserName + "] ";
            }
            return strListUsersInRole;
        }

        public async Task OnGetAsync()
        {
            foreach (var a in _context.Users)
            {
                if (await _userManager.IsInRoleAsync(a, "User") && !await _userManager.IsInRoleAsync(a, "Trainer"))
                {
                    staffuserPermit.Add(a.UserName);
                }
            }

            IQueryable<string> RoleQuery = from m in _roleManager.Roles orderby m.Name select m.Name;
            IQueryable<string> StaffRoleQuery = from n in _roleManager.Roles where staffrolePermit.Contains(n.Name) orderby n.Name select n.Name;
            IQueryable<string> UsersQuery = from u in _context.Users orderby u.UserName select u.UserName;
            IQueryable<string> StaffUsersQuery = from v in _context.Users where staffuserPermit.Contains(v.UserName) orderby v.UserName select v.UserName;

            RolesSelectList = new SelectList(await RoleQuery.Distinct().ToListAsync());
            StaffRolesSelectList = new SelectList(await StaffRoleQuery.Distinct().ToListAsync());
            UsersSelectList = new SelectList(await UsersQuery.Distinct().ToListAsync());
            StaffUsersSelectList = new SelectList(await StaffUsersQuery.Distinct().ToListAsync());


            // Get all the roles 
            var roles = from r in _roleManager.Roles
                        select r;
            Listroles = await roles.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string selectedusername, string selectedrolename)
        {
            if ((selectedusername == null) || (selectedrolename == null))
            {
                return RedirectToPage("Manage");
            }

            ApplicationUser AppUser = _context.Users.SingleOrDefault(u => u.UserName == selectedusername);
            ApplicationRole AppRole = null;
            if (User.IsInRole("Admin"))
            {
                AppRole = await _roleManager.FindByNameAsync(selectedrolename);
            }
            else if (User.IsInRole("Staff"))
            {
                if (staffrolePermit.Contains(selectedrolename))
                {
                    AppRole = await _roleManager.FindByNameAsync(selectedrolename);
                }
                else
                {
                    return RedirectToPage("../Error");
                }
            }
            else
            {
                return RedirectToPage("../Error");
            }

            IdentityResult roleResult = await _userManager.AddToRoleAsync(AppUser, AppRole.Name);

            if (roleResult.Succeeded)
            {
                TempData["message"] = "Role added to this user successfully";
                return RedirectToPage("Manage");
            }

            return RedirectToPage("Manage");
        }

        public async Task<IActionResult> OnPostDeleteUserRoleAsync(string delusername, string delrolename)
        {
            //When the Delete this user from  Role button is pressed 
            if ((delusername == null) || (delrolename == null))
            {
                return RedirectToPage("Manage");
            }

            ApplicationUser user = _context.Users.Where(u => u.UserName == delusername).FirstOrDefault();

            if (await _userManager.IsInRoleAsync(user, delrolename))
            {
                await _userManager.RemoveFromRoleAsync(user, delrolename);

                TempData["message"] = "Role removed from this user successfully";
            }

            return RedirectToPage("Manage");
        }

    }
}
