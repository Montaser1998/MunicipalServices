using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MunicipalServices.Models;

namespace MunicipalServices.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<Data.Users> _user;

        public HomeController(ILogger<HomeController> logger, Data.ApplicationDbContext context, UserManager<Data.Users> user)
        {
            _logger = logger;
            _context = context;
            _user = user;
        }

        public IActionResult Index()
        {
            var userid = _user.GetUserId(User);
            if (this.User.IsInRole("الادارة") || this.User.IsInRole("قسم المالية"))
            {
                ViewData["CatchReceipts"] = _context.CatchReceipts.Where(c => c.UserID == userid && c.Deleted ==false).Count();
                ViewData["ComplaintForm"] = _context.ComplaintForm.Where(c => c.UserID == userid && c.Deleted == false).Count();
                ViewData["Receipts"] = _context.Receipts.Where(c => c.UserID == userid && c.Deleted == false).Count();
                ViewData["CraftAndIndustryLicense"] = _context.CraftAndIndustryLicense.Where(c => c.UserID == userid && c.Deleted == false).Count();
            }
            if (this.User.IsInRole("الادارة") || this.User.IsInRole("قسم الهندسة"))
                ViewData["ConstructionLicense"] = _context.ConstructionLicense.Where(c => c.UserID == userid && c.Deleted == false).Count();

            if (this.User.IsInRole("قسم المياه") || this.User.IsInRole("الادارة"))
                ViewData["WaterMeterSubscriptionRequest"] = _context.WaterMeterSubscriptionRequest.Where(c => c.UserID == userid && c.Deleted == false).Count();

            if (this.User.IsInRole("قسم الهندسة") || this.User.IsInRole("قسم المياه") || this.User.IsInRole("الادارة") || this.User.IsInRole("قسم المالية"))
                ViewData["VacationRequest"] = _context.VacationRequest.Where(c => c.UserID == userid && c.Deleted == false).Count();

            if (this.User.IsInRole("الادارة"))
                ViewData["Users"] = _context.Users.Where(c => c.Deleted == false).Count();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
