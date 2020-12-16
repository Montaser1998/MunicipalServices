using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalServices.Data;

namespace MunicipalServices.Controllers
{
    [Authorize(Roles = "قسم المالية,الادارة")]
    public class CraftAndIndustryLicensesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Data.Users> usermanager;


        public CraftAndIndustryLicensesController(ApplicationDbContext context , Microsoft.AspNetCore.Identity.UserManager<Data.Users> user)
        {
            _context = context;
            usermanager = user;

        }

        // GET: CraftAndIndustryLicenses
        public ViewResult Index()
        {
            var applicationDbContext = _context.CraftAndIndustryLicense.Include(c => c.User).Where(c => c.Deleted == false); 
            return View(applicationDbContext);
        }

        // GET: CraftAndIndustryLicenses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craftAndIndustryLicense = await _context.CraftAndIndustryLicense
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (craftAndIndustryLicense == null)
            {
                return NotFound();
            }

            return View(craftAndIndustryLicense);
        }

        // GET: CraftAndIndustryLicenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CraftAndIndustryLicenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LicenseHolderName,IdentificationNumber,CraftOrIndustryType,ClassifiedInTail,Address,Ends,LicenseFee,VoucherNumber,ID,CreatedDate,UpdatedDate,UserID,Deleted")] CraftAndIndustryLicense craftAndIndustryLicense)
        {
            if (ModelState.IsValid)
            {
                var datetime = DateTime.UtcNow;
                craftAndIndustryLicense.ID = Guid.NewGuid();
                craftAndIndustryLicense.CreatedDate = datetime;
                craftAndIndustryLicense.UpdatedDate = datetime;
                craftAndIndustryLicense.UserID = usermanager.GetUserId(User);
                _context.Add(craftAndIndustryLicense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(craftAndIndustryLicense);
        }

        // GET: CraftAndIndustryLicenses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craftAndIndustryLicense = await _context.CraftAndIndustryLicense.FindAsync(id);
            if (craftAndIndustryLicense == null)
            {
                return NotFound();
            }
            return View(craftAndIndustryLicense);
        }

        // POST: CraftAndIndustryLicenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LicenseHolderName,IdentificationNumber,CraftOrIndustryType,ClassifiedInTail,Address,Ends,LicenseFee,VoucherNumber,ID,CreatedDate,UpdatedDate,UserID,Deleted")] CraftAndIndustryLicense craftAndIndustryLicense)
        {
            if (id != craftAndIndustryLicense.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    craftAndIndustryLicense.UpdatedDate = DateTime.UtcNow;
                    _context.Update(craftAndIndustryLicense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CraftAndIndustryLicenseExists(craftAndIndustryLicense.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", craftAndIndustryLicense.UserID);
            return View(craftAndIndustryLicense);
        }

        // GET: CraftAndIndustryLicenses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craftAndIndustryLicense = await _context.CraftAndIndustryLicense
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (craftAndIndustryLicense == null)
            {
                return NotFound();
            }

            return View(craftAndIndustryLicense);
        }

        // POST: CraftAndIndustryLicenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var craftAndIndustryLicense = await _context.CraftAndIndustryLicense.FindAsync(id);
            craftAndIndustryLicense.Deleted = true;
            _context.CraftAndIndustryLicense.Update(craftAndIndustryLicense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CraftAndIndustryLicenseExists(Guid id)
        {
            return _context.CraftAndIndustryLicense.Any(e => e.ID == id);
        }
    }
}
