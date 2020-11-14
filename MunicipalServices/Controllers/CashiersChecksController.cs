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
    public class CashiersChecksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Data.Users> usermanager;

        public CashiersChecksController(ApplicationDbContext context, UserManager<Data.Users> user)
        {
            _context = context;
            usermanager = user;
        }

        // GET: CashiersChecks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CashiersCheck.Include(c => c.CatchReceipt).Include(c => c.Receipt).Include(c => c.User).Where(c=>c.Deleted==false);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CashiersChecks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashiersCheck = await _context.CashiersCheck
                .Include(c => c.CatchReceipt)
                .Include(c => c.Receipt)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cashiersCheck == null)
            {
                return NotFound();
            }

            return View(cashiersCheck);
        }

        // GET: CashiersChecks/Create
        public IActionResult Create()
        {
            ViewData["CatchReceiptID"] = new SelectList(_context.CatchReceipts, "ID", "FullName");
            ViewData["ReceiptID"] = new SelectList(_context.Receipts, "ID", "FullName");
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: CashiersChecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatchReceiptID,ReceiptID,NumberID,CheckNumber,AccountNumber,CodeBank,BankName,CodeBranch,BranchName,DateOfTheWorthy,AmountOfMoney,ID,CreatedDate,UpdatedDate,UserID,Deleted")] CashiersCheck cashiersCheck)
        {
            if (ModelState.IsValid)
            {
                var datetime = DateTime.UtcNow;
                cashiersCheck.ID = Guid.NewGuid();
                cashiersCheck.CreatedDate = datetime;
                cashiersCheck.UpdatedDate = datetime;
                cashiersCheck.UserID = usermanager.GetUserId(User);
                _context.Add(cashiersCheck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatchReceiptID"] = new SelectList(_context.CatchReceipts, "ID", "FullName", cashiersCheck.CatchReceiptID);
            ViewData["ReceiptID"] = new SelectList(_context.Receipts, "ID", "FullName", cashiersCheck.ReceiptID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", cashiersCheck.UserID);
            return View(cashiersCheck);
        }

        // GET: CashiersChecks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashiersCheck = await _context.CashiersCheck.FindAsync(id);
            if (cashiersCheck == null)
            {
                return NotFound();
            }
            ViewData["CatchReceiptID"] = new SelectList(_context.CatchReceipts, "ID", "FullName", cashiersCheck.CatchReceiptID);
            ViewData["ReceiptID"] = new SelectList(_context.Receipts, "ID", "FullName", cashiersCheck.ReceiptID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", cashiersCheck.UserID);
            return View(cashiersCheck);
        }

        // POST: CashiersChecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CatchReceiptID,ReceiptID,NumberID,CheckNumber,AccountNumber,CodeBank,BankName,CodeBranch,BranchName,DateOfTheWorthy,AmountOfMoney,ID,CreatedDate,UpdatedDate,UserID,Deleted")] CashiersCheck cashiersCheck)
        {
            if (id != cashiersCheck.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cashiersCheck.UpdatedDate = DateTime.UtcNow;
                    _context.Update(cashiersCheck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashiersCheckExists(cashiersCheck.ID))
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
            ViewData["CatchReceiptID"] = new SelectList(_context.CatchReceipts, "ID", "FullName", cashiersCheck.CatchReceiptID);
            ViewData["ReceiptID"] = new SelectList(_context.Receipts, "ID", "FullName", cashiersCheck.ReceiptID);
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", cashiersCheck.UserID);
            return View(cashiersCheck);
        }

        // GET: CashiersChecks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashiersCheck = await _context.CashiersCheck
                .Include(c => c.CatchReceipt)
                .Include(c => c.Receipt)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cashiersCheck == null)
            {
                return NotFound();
            }

            return View(cashiersCheck);
        }

        // POST: CashiersChecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cashiersCheck = await _context.CashiersCheck.FindAsync(id);
            cashiersCheck.Deleted = true;
            _context.CashiersCheck.Update(cashiersCheck);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashiersCheckExists(Guid id)
        {
            return _context.CashiersCheck.Any(e => e.ID == id);
        }
    }
}
