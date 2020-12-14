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
    [Authorize(Roles = "قسم الهندسة,الادارة,قسم المالية,قسم المياه")]
    public class VacationRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Data.Users> usermanager;

        public VacationRequestsController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<Data.Users> user)
        {
            _context = context;
            usermanager = user;
        }

        // GET: VacationRequests
        public ViewResult Index()
        {
            var applicationDbContext = _context.VacationRequest.Include(v => v.User).Where(v => v.Deleted == false);
            return View(applicationDbContext);
        }

        // GET: VacationRequests/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequest
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            return View(vacationRequest);
        }

        // GET: VacationRequests/Create
        public IActionResult Create()
        {
            ViewData["VacationType"] = new SelectList(from VacationType d in Enum.GetValues(typeof(VacationType)) select new { ID = (int)d, Name = Helper.Enumerations.GetEnumDescription(d) }, "ID", "Name");
            return View();
        }

        // POST: VacationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DaysVacation,StartVacationDate,EndVacationDate,City,Street,PhoneNumber,NameAssignee,Agree,VacationType")] VacationRequest vacationRequest)
        {
            if (ModelState.IsValid)
            {
                var datetime = DateTime.UtcNow;
                vacationRequest.ID = Guid.NewGuid();
                vacationRequest.CreatedDate = datetime;
                vacationRequest.UpdatedDate = datetime;
                vacationRequest.UserID = usermanager.GetUserId(User);
                _context.Add(vacationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vacationRequest);
        }

        // GET: VacationRequests/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequest.FindAsync(id);
            if (vacationRequest == null)
            {
                return NotFound();
            }
            ViewData["VacationType"] = new SelectList(from VacationType d in Enum.GetValues(typeof(VacationType)) select new { ID = (int)d, Name = Helper.Enumerations.GetEnumDescription(d) }, "ID", "Name");
            return View(vacationRequest);
        }

        // POST: VacationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DaysVacation,StartVacationDate,EndVacationDate,City,Street,PhoneNumber,NameAssignee,Agree,VacationType,ID,CreatedDate,UserID")] VacationRequest vacationRequest)
        {
            if (id != vacationRequest.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vacationRequest.UpdatedDate = DateTime.UtcNow;
                    _context.Update(vacationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationRequestExists(vacationRequest.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", vacationRequest.UserID);
             return View(vacationRequest);
        }

        // GET: VacationRequests/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacationRequest = await _context.VacationRequest
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vacationRequest == null)
            {
                return NotFound();
            }

            return View(vacationRequest);
        }

        // POST: VacationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vacationRequest = await _context.VacationRequest.FindAsync(id);
            vacationRequest.Deleted = true;
            _context.VacationRequest.Update(vacationRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationRequestExists(Guid id)
        {
            return _context.VacationRequest.Any(e => e.ID == id);
        }
    }
}
