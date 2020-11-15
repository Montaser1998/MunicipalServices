using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalServices.Data;

namespace MunicipalServices.Controllers
{
    public class LicenseHolderInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Data.Users> _userManager;

        public LicenseHolderInformationsController(ApplicationDbContext context, UserManager<Data.Users> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: LicenseHolderInformations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LicenseHolderInformation.Include(l => l.ConstructionLicense).Include(l => l.User).Where(l=>l.Deleted == false);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LicenseHolderInformations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var licenseHolderInformation = await _context.LicenseHolderInformation
                .Include(l => l.ConstructionLicense)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (licenseHolderInformation == null)
            {
                return NotFound();
            }

            return View(licenseHolderInformation);
        }

        // GET: LicenseHolderInformations/Create
        public IActionResult Create()
        {
            ViewData["ConstructionLicenseID"] = new SelectList(_context.ConstructionLicense, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: LicenseHolderInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConstructionLicenseID,NameLicenseHolder,IdentityNumberLicenseHolder,AddressLicenseHolder,PhoneNumberLicenseHolder,NameDesigningOffice,IdentityNumberDesignerOffice,AddressDesignerOffice,PhoneNumberDesignerOffice,NameSupervisingEngineer,IdentityNumberSupervisingEngineer,AddressSupervisingEngineer,PhoneNumberSupervisingEngineer,ID,CreatedDate,UpdatedDate,UserID,Deleted")] LicenseHolderInformation licenseHolderInformation)
        {
            if (ModelState.IsValid)
            {
                var datetime = DateTime.UtcNow;
                licenseHolderInformation.ID = Guid.NewGuid();
                licenseHolderInformation.CreatedDate = datetime;
                licenseHolderInformation.UpdatedDate = datetime;
                licenseHolderInformation.UserID = _userManager.GetUserId(User);
                _context.Add(licenseHolderInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConstructionLicenseID"] = new SelectList(_context.ConstructionLicense, "ID", "ID", licenseHolderInformation.ConstructionLicenseID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", licenseHolderInformation.UserID);
            return View(licenseHolderInformation);
        }

        // GET: LicenseHolderInformations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var licenseHolderInformation = await _context.LicenseHolderInformation.FindAsync(id);
            if (licenseHolderInformation == null)
            {
                return NotFound();
            }
            ViewData["ConstructionLicenseID"] = new SelectList(_context.ConstructionLicense, "ID", "ID", licenseHolderInformation.ConstructionLicenseID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", licenseHolderInformation.UserID);
            return View(licenseHolderInformation);
        }

        // POST: LicenseHolderInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ConstructionLicenseID,NameLicenseHolder,IdentityNumberLicenseHolder,AddressLicenseHolder,PhoneNumberLicenseHolder,NameDesigningOffice,IdentityNumberDesignerOffice,AddressDesignerOffice,PhoneNumberDesignerOffice,NameSupervisingEngineer,IdentityNumberSupervisingEngineer,AddressSupervisingEngineer,PhoneNumberSupervisingEngineer,ID,CreatedDate,UpdatedDate,UserID,Deleted")] LicenseHolderInformation licenseHolderInformation)
        {
            if (id != licenseHolderInformation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    licenseHolderInformation.UpdatedDate = DateTime.UtcNow;
                    _context.Update(licenseHolderInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LicenseHolderInformationExists(licenseHolderInformation.ID))
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
            ViewData["ConstructionLicenseID"] = new SelectList(_context.ConstructionLicense, "ID", "ID", licenseHolderInformation.ConstructionLicenseID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", licenseHolderInformation.UserID);
            return View(licenseHolderInformation);
        }

        // GET: LicenseHolderInformations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var licenseHolderInformation = await _context.LicenseHolderInformation
                .Include(l => l.ConstructionLicense)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (licenseHolderInformation == null)
            {
                return NotFound();
            }

            return View(licenseHolderInformation);
        }

        // POST: LicenseHolderInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var licenseHolderInformation = await _context.LicenseHolderInformation.FindAsync(id);
            licenseHolderInformation.Deleted = true;
            _context.LicenseHolderInformation.Update(licenseHolderInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LicenseHolderInformationExists(Guid id)
        {
            return _context.LicenseHolderInformation.Any(e => e.ID == id);
        }
    }
}
