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
    [Authorize(Roles = "قسم المالية,الادارة")]
    public class CatchReceiptsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Data.Users> _userManager;

        public CatchReceiptsController(ApplicationDbContext context, UserManager<Data.Users> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: CatchReceipts
        public ViewResult Index()
        {
            var applicationDbContext = _context.CatchReceipts.Include(c => c.User).Where(c=>c.Deleted == false);
            return View(applicationDbContext);
        }

        // GET: CatchReceipts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catchReceipts = await _context.CatchReceipts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (catchReceipts == null)
            {
                return NotFound();
            }

            return View(catchReceipts);
        }

        // GET: CatchReceipts/Create
        public IActionResult Create()
        {
            ViewData["CurrencyType"] = new SelectList(from CurrencyType d in Enum.GetValues(typeof(CurrencyType)) select new { ID = (int)d, Name = Helper.Enumerations.GetEnumDescription(d) }, "ID", "Name");
            return View();
        }

        // POST: CatchReceipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,AmountOfMoneyNumber,AmountOfMoneyText,Reason,ToAccount,Currency")] CatchReceipts catchReceipts)
        {
            if (ModelState.IsValid)
            {
                var dateTime = DateTime.UtcNow;
                catchReceipts.ID = Guid.NewGuid();
                catchReceipts.CreatedDate = dateTime;
                catchReceipts.UpdatedDate = dateTime;
                catchReceipts.UserID = _userManager.GetUserId(User);
                _context.Add(catchReceipts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catchReceipts);
        }

        // GET: CatchReceipts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catchReceipts = await _context.CatchReceipts.FindAsync(id);
            if (catchReceipts == null)
            {
                return NotFound();
            }
            ViewData["CurrencyType"] = new SelectList(from CurrencyType d in Enum.GetValues(typeof(CurrencyType)) select new { ID = (int)d, Name = Helper.Enumerations.GetEnumDescription(d) }, "ID", "Name");
            return View(catchReceipts);
        }

        // POST: CatchReceipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FullName,AmountOfMoneyNumber,AmountOfMoneyText,Reason,ToAccount,Currency")] CatchReceipts catchReceipts)
        {
            if (id != catchReceipts.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    catchReceipts.UpdatedDate = DateTime.UtcNow;

                    _context.Update(catchReceipts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatchReceiptsExists(catchReceipts.ID))
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
            return View(catchReceipts);
        }

        // GET: CatchReceipts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catchReceipts = await _context.CatchReceipts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (catchReceipts == null)
            {
                return NotFound();
            }

            return View(catchReceipts);
        }

        // POST: CatchReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var catchReceipts = await _context.CatchReceipts.FindAsync(id);
            catchReceipts.Deleted = true;
            _context.CatchReceipts.Update(catchReceipts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatchReceiptsExists(Guid id)
        {
            return _context.CatchReceipts.Any(e => e.ID == id);
        }
    }
}
