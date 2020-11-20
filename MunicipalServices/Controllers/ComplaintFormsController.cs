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
    public class ComplaintFormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Data.Users> usermanager;

        public ComplaintFormsController(ApplicationDbContext context, Microsoft.AspNetCore.Identity. UserManager<Data.Users> user)
        {
            _context = context;
            usermanager = user;
        }

        // GET: ComplaintForms
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ComplaintForm.Include(c => c.User).Where(c => c.Deleted == false);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ComplaintForms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintForm = await _context.ComplaintForm
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complaintForm == null)
            {
                return NotFound();
            }

            return View(complaintForm);
        }

        // GET: ComplaintForms/Create
        public IActionResult Create()
        {
            ViewData["ComplaintType"] = new SelectList(from ComplaintType d in Enum.GetValues(typeof(ComplaintType)) select new { ID = (int)d, Name = Helper.Enumerations.GetEnumDescription(d) }, "ID", "Name");
            return View();
        }

        // POST: ComplaintForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber,IdentificationNumber,AreaName,FullAddress,ComplaintType,SubjectComplaint,ID,CreatedDate,UpdatedDate,UserID,Deleted")] ComplaintForm complaintForm)
        {
            if (ModelState.IsValid)
            {
                var datetime = DateTime.UtcNow;
                complaintForm.ID = Guid.NewGuid();
                complaintForm.CreatedDate = datetime;
                complaintForm.UpdatedDate = datetime;
                complaintForm.UserID = usermanager.GetUserId(User);
                _context.Add(complaintForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(complaintForm);
        }

        // GET: ComplaintForms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintForm = await _context.ComplaintForm.FindAsync(id);
            if (complaintForm == null)
            {
                return NotFound();
            }
            ViewData["ComplaintType"] = new SelectList(from ComplaintType d in Enum.GetValues(typeof(ComplaintType)) select new { ID = (int)d, Name = Helper.Enumerations.GetEnumDescription(d) }, "ID", "Name");
            return View(complaintForm);
        }

        // POST: ComplaintForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FullName,PhoneNumber,IdentificationNumber,AreaName,FullAddress,ComplaintType,SubjectComplaint,ID,CreatedDate,UpdatedDate,UserID,Deleted")] ComplaintForm complaintForm)
        {
            if (id != complaintForm.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    complaintForm.UpdatedDate = DateTime.UtcNow;
                    _context.Update(complaintForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintFormExists(complaintForm.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", complaintForm.UserID);
            return View(complaintForm);
        }

        // GET: ComplaintForms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complaintForm = await _context.ComplaintForm
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (complaintForm == null)
            {
                return NotFound();
            }

            return View(complaintForm);
        }

        // POST: ComplaintForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var complaintForm = await _context.ComplaintForm.FindAsync(id);
            complaintForm.Deleted = true;
            _context.ComplaintForm.Update(complaintForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintFormExists(Guid id)
        {
            return _context.ComplaintForm.Any(e => e.ID == id);
        }
    }
}
