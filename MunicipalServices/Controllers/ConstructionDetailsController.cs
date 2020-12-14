using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalServices.Data;

namespace MunicipalServices.Controllers
{
    [Authorize(Roles = "قسم الهندسة,الادارة")]
    public class ConstructionDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public ConstructionDetailsController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ConstructionDetails
        public ViewResult Index()
        {
            var applicationDbContext = _context.ConstructionDetails.Include(c => c.ConstructionLicense).Include(c => c.User).Where(c => c.Deleted == false);
            return View(applicationDbContext);
        }

        // GET: ConstructionDetails/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructionDetails = await _context.ConstructionDetails
                .Include(c => c.ConstructionLicense)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (constructionDetails == null)
            {
                return NotFound();
            }

            return View(constructionDetails);
        }

        // GET: ConstructionDetails/Create
        public IActionResult Create(Guid id)
        {
            var model = new ConstructionDetails();
            model.ConstructionLicenseID = id;
            return View(model);
        }

        // POST: ConstructionDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConstructionLicenseID,ConstructionDescription,Amount,MeasruingUnit,UnitPrice,Total,ID,CreatedDate,UpdatedDate,UserID,Deleted")] ConstructionDetails constructionDetails)
        {
            if (ModelState.IsValid)
            {
                var dateTime = DateTime.UtcNow;
                constructionDetails.ID = Guid.NewGuid();
                constructionDetails.CreatedDate = dateTime;
                constructionDetails.UpdatedDate = dateTime;
                constructionDetails.Total = constructionDetails.Amount * constructionDetails.UnitPrice;
                constructionDetails.UserID = _userManager.GetUserId(User);
                _context.Add(constructionDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConstructionLicenseID"] = new SelectList(_context.ConstructionLicense, "ID", "ID", constructionDetails.ConstructionLicenseID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", constructionDetails.UserID);
            return View(constructionDetails);
        }

        // GET: ConstructionDetails/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructionDetails = await _context.ConstructionDetails.FindAsync(id);
            if (constructionDetails == null)
            {
                return NotFound();
            }
            ViewData["ConstructionLicenseID"] = new SelectList(_context.ConstructionLicense, "ID", "ID", constructionDetails.ConstructionLicenseID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", constructionDetails.UserID);
            return View(constructionDetails);
        }

        // POST: ConstructionDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ConstructionLicenseID,ConstructionDescription,Amount,MeasruingUnit,UnitPrice,Total,ID,CreatedDate,UpdatedDate,UserID,Deleted")] ConstructionDetails constructionDetails)
        {
            if (id != constructionDetails.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    constructionDetails.UpdatedDate = DateTime.UtcNow;
                    constructionDetails.Total = constructionDetails.Amount * constructionDetails.UnitPrice;
                    _context.Update(constructionDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConstructionDetailsExists(constructionDetails.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConstructionLicenseID"] = new SelectList(_context.ConstructionLicense, "ID", "ID", constructionDetails.ConstructionLicenseID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", constructionDetails.UserID);
            return View(constructionDetails);
        }

        // GET: ConstructionDetails/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructionDetails = await _context.ConstructionDetails
                .Include(c => c.ConstructionLicense)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (constructionDetails == null)
            {
                return NotFound();
            }

            return View(constructionDetails);
        }

        // POST: ConstructionDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var constructionDetails = await _context.ConstructionDetails.FindAsync(id);
            constructionDetails.Deleted = true;
            _context.ConstructionDetails.Update(constructionDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConstructionDetailsExists(Guid id)
        {
            return _context.ConstructionDetails.Any(e => e.ID == id);
        }
    }
}
