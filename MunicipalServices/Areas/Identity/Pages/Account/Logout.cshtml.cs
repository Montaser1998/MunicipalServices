using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MunicipalServices.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Data.Users> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<Data.Users> _userManager;


        public LogoutModel(SignInManager<Data.Users> signInManager, UserManager<Data.Users> userManager, ILogger<LogoutModel> logger, Data.ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            var user = await _userManager.GetUserAsync(User);
            user.LastDateLogined = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
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
