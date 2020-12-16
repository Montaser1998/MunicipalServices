using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MunicipalServices.Areas.Identity.Pages.Account
{
    public class UserRolesModel : PageModel
    {
        private readonly UserManager<Data.Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesModel(UserManager<Data.Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

        }
        [BindProperty]
        public List<UserRolesViewModel> UserRoles { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        public async Task OnGet(string userId)
        {
            UserRoles = new List<UserRolesViewModel>();
            UserId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return;
            }
            UserName = user.FullName;
            var roles = _roleManager.Roles.ToList();
            foreach (var role in roles)
            {
                try
                {
                    var userRolesViewModel = new UserRolesViewModel
                    {
                        RoleId = role.Id,
                        RoleName = role.Name
                    };
                    var result = await _userManager.IsInRoleAsync(user, role.Name);
                    if (result)
                    {
                        userRolesViewModel.Selected = true;
                    }
                    else
                    {
                        userRolesViewModel.Selected = false;
                    }
                    UserRoles.Add(userRolesViewModel);
                }
                catch (Exception ex)
                {

                }
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                await OnGet(UserId);
            }
            result = await _userManager.AddToRolesAsync(user, UserRoles.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                await OnGet(UserId);
            }
            return RedirectToAction("Index", "UserManagement");
        }
    }
    public class UserRolesViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
    }
}
