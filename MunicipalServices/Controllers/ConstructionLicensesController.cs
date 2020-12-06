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
    public class ConstructionLicensesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Data.Users> _userManager;

        public ConstructionLicensesController(ApplicationDbContext context, UserManager<Data.Users> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: ConstructionLicenses
        public ViewResult Index()
        {
            var applicationDbContext = _context.ConstructionLicense.Include(c => c.User).Where(c=>c.Deleted == false);
            return View(applicationDbContext);
        }

        // GET: ConstructionLicenses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructionLicense = await _context.ConstructionLicense
                .Include(c => c.LicenseHolderInformation)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (constructionLicense == null)
            {
                return NotFound();
            }

            return View(constructionLicense);
        }

        // GET: ConstructionLicenses/Create
        public IActionResult Create()
        {
            ViewData["ConstructionLicenseType"] = new SelectList(from ConstructionLicenseType d in Enum.GetValues(typeof(ConstructionLicenseType)) select new { ID = (int)d, Name = Helper.Enumerations.GetEnumDescription(d) }, "ID", "Name");
            ViewData["BillOfFeesID"] = new SelectList(_context.CatchReceipts, "ID", "FullName");
            ViewData["BillRemainingFeesID"] = new SelectList(_context.CatchReceipts, "ID", "FullName");
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            var model = new ConstructionLicense();
            model.LicenseHolderInformation = new LicenseHolderInformation();
            return View(model);
        }

        // POST: ConstructionLicenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateOfIssuance,FileNumber,ValidatedBySessionNumber,TownName,District,Street,Basin,Part,ConstructionUse,ConstructionLicenseType,LicenseHolderInformation,LocalCommitteeNumber,DateApprovalLocalCommittee,LicenseDescription,LicenseConditions,FeeDate,RemainingFeesDate,BillOfFeesID,BillRemainingFeesID")] ConstructionLicense constructionLicense)
        {
            if (ModelState.IsValid)
            {
                var dateTime = DateTime.UtcNow;
                constructionLicense.ID = Guid.NewGuid();
                constructionLicense.CreatedDate = dateTime;
                constructionLicense.UpdatedDate = dateTime;
                constructionLicense.UserID = _userManager.GetUserId(User);
                constructionLicense.LicenseHolderInformation.ConstructionLicenseID = constructionLicense.ID;
                constructionLicense.LicenseHolderInformation.CreatedDate = dateTime;
                constructionLicense.LicenseHolderInformation.UpdatedDate = dateTime;
                constructionLicense.LicenseHolderInformation.UserID = _userManager.GetUserId(User);

                _context.Add(constructionLicense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(constructionLicense);
        }

        // GET: ConstructionLicenses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructionLicense = await _context.ConstructionLicense.Include(c => c.LicenseHolderInformation).Where(c=>c.ID == id).SingleOrDefaultAsync();
            if (constructionLicense == null)
            {
                return NotFound();
            }
            ViewData["ConstructionLicenseType"] = new SelectList(from ConstructionLicenseType d in Enum.GetValues(typeof(ConstructionLicenseType)) select new { ID = (int)d, Name = Helper.Enumerations.GetEnumDescription(d) }, "ID", "Name");
            ViewData["BillOfFeesID"] = new SelectList(_context.CatchReceipts, "ID", "FullName", constructionLicense.BillOfFeesID);
            ViewData["BillRemainingFeesID"] = new SelectList(_context.CatchReceipts, "ID", "FullName", constructionLicense.BillRemainingFeesID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", constructionLicense.UserID);
            return View(constructionLicense);
        }

        // POST: ConstructionLicenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DateOfIssuance,FileNumber,ValidatedBySessionNumber,TownName,District,Street,Basin,Part,ConstructionUse,ConstructionLicenseType,LicenseHolderInformation,LocalCommitteeNumber,DateApprovalLocalCommittee,LicenseDescription,LicenseConditions,FeeDate,RemainingFeesDate,BillOfFeesID,BillRemainingFeesID,ID,CreatedDate,UserID")] ConstructionLicense constructionLicense)
        {
            if (id != constructionLicense.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    constructionLicense.UpdatedDate = DateTime.UtcNow;

                    _context.Update(constructionLicense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConstructionLicenseExists(constructionLicense.ID))
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
            return View(constructionLicense);
        }

        // GET: ConstructionLicenses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var constructionLicense = await _context.ConstructionLicense
                .Include(c => c.LicenseHolderInformation)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (constructionLicense == null)
            {
                return NotFound();
            }

            return View(constructionLicense);
        }

        // POST: ConstructionLicenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var constructionLicense = await _context.ConstructionLicense.FindAsync(id);
            constructionLicense.Deleted = true;
            _context.ConstructionLicense.Update(constructionLicense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConstructionLicenseExists(Guid id)
        {
            return _context.ConstructionLicense.Any(e => e.ID == id);
        }
    }
}
