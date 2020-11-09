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
    public class ReceiptsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceiptsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Receipts.Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipts = await _context.Receipts
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (receipts == null)
            {
                return NotFound();
            }

            return View(receipts);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,AmountOfMoneyNumber,AmountOfMoneyText,Reason,OnAccount,ReceivedFrom,Currency,ID,CreatedDate,UpdatedDate,UserID,Deleted")] Receipts receipts)
        {
            if (ModelState.IsValid)
            {
                receipts.ID = Guid.NewGuid();
                _context.Add(receipts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", receipts.UserID);
            return View(receipts);
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipts = await _context.Receipts.FindAsync(id);
            if (receipts == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", receipts.UserID);
            return View(receipts);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FullName,AmountOfMoneyNumber,AmountOfMoneyText,Reason,OnAccount,ReceivedFrom,Currency,ID,CreatedDate,UpdatedDate,UserID,Deleted")] Receipts receipts)
        {
            if (id != receipts.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptsExists(receipts.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", receipts.UserID);
            return View(receipts);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipts = await _context.Receipts
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (receipts == null)
            {
                return NotFound();
            }

            return View(receipts);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var receipts = await _context.Receipts.FindAsync(id);
            _context.Receipts.Remove(receipts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptsExists(Guid id)
        {
            return _context.Receipts.Any(e => e.ID == id);
        }
    }
}
