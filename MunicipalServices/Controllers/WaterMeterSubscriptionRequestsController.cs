using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MunicipalServices.Data;

namespace MunicipalServices.Controllers
{
    public class WaterMeterSubscriptionRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Data.Users> usermanager;


        public WaterMeterSubscriptionRequestsController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<Data.Users> user)
        {
            _context = context;
            usermanager = user;
        }

        // GET: WaterMeterSubscriptionRequests
        public ViewResult Index()
        {
            var applicationDbContext = _context.WaterMeterSubscriptionRequest.Include(w => w.User).Where(w => w.Deleted == false);
            return View(applicationDbContext);
        }

        // GET: WaterMeterSubscriptionRequests/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterMeterSubscriptionRequest = await _context.WaterMeterSubscriptionRequest
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (waterMeterSubscriptionRequest == null)
            {
                return NotFound();
            }

            return View(waterMeterSubscriptionRequest);
        }

        // GET: WaterMeterSubscriptionRequests/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: WaterMeterSubscriptionRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Location,PhoneNumber,BasinNumber,PieceNumber,WaterOfficialSuggestions,MunicipalityDecision,ID,CreatedDate,UpdatedDate,UserID,Deleted")] WaterMeterSubscriptionRequest waterMeterSubscriptionRequest)
        {
            if (ModelState.IsValid)
            {
                var datetime = DateTime.UtcNow;
                waterMeterSubscriptionRequest.ID = Guid.NewGuid();
                waterMeterSubscriptionRequest.CreatedDate = datetime;
                waterMeterSubscriptionRequest.UpdatedDate = datetime;
                waterMeterSubscriptionRequest.UserID = usermanager.GetUserId(User);
                _context.Add(waterMeterSubscriptionRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(waterMeterSubscriptionRequest);
        }

        // GET: WaterMeterSubscriptionRequests/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterMeterSubscriptionRequest = await _context.WaterMeterSubscriptionRequest.FindAsync(id);
            if (waterMeterSubscriptionRequest == null)
            {
                return NotFound();
            }
            return View(waterMeterSubscriptionRequest);
        }

        // POST: WaterMeterSubscriptionRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Location,PhoneNumber,BasinNumber,PieceNumber,WaterOfficialSuggestions,MunicipalityDecision,ID,CreatedDate,UpdatedDate,UserID,Deleted")] WaterMeterSubscriptionRequest waterMeterSubscriptionRequest)
        {
            if (id != waterMeterSubscriptionRequest.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    waterMeterSubscriptionRequest.UpdatedDate = DateTime.UtcNow;
                    _context.Update(waterMeterSubscriptionRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WaterMeterSubscriptionRequestExists(waterMeterSubscriptionRequest.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", waterMeterSubscriptionRequest.UserID);
            return View(waterMeterSubscriptionRequest);
        }

        // GET: WaterMeterSubscriptionRequests/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var waterMeterSubscriptionRequest = await _context.WaterMeterSubscriptionRequest
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (waterMeterSubscriptionRequest == null)
            {
                return NotFound();
            }

            return View(waterMeterSubscriptionRequest);
        }

        // POST: WaterMeterSubscriptionRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var waterMeterSubscriptionRequest = await _context.WaterMeterSubscriptionRequest.FindAsync(id);
            waterMeterSubscriptionRequest.Deleted = true;
            _context.WaterMeterSubscriptionRequest.Update(waterMeterSubscriptionRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WaterMeterSubscriptionRequestExists(Guid id)
        {
            return _context.WaterMeterSubscriptionRequest.Any(e => e.ID == id);
        }
    }
}
