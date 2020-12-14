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
        public ViewResult Index()
        {
            var applicationDbContext = _context.CashiersCheck.Include(c => c.CatchReceipt).Include(c => c.Receipt).Include(c => c.User).Where(c => c.Deleted == false);
            return View(applicationDbContext);
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
        public IActionResult Create(Guid id, bool IsCatchReceipts)
        {
            var model = new Data.CashiersCheck();
            if (IsCatchReceipts)
            {
                model.CatchReceiptID = id; 
            }
            else
            {
                model.ReceiptID = id;
            }
            return View(model);
        }

        // POST: CashiersChecks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatchReceiptID,ReceiptID,CheckNumber,AccountNumber,CodeBank,BankName,CodeBranch,BranchName,DateOfTheWorthy,AmountOfMoney")] CashiersCheck cashiersCheck)
        {
            if (ModelState.IsValid)
            {
                var datetime = DateTime.UtcNow;
                cashiersCheck.ID = Guid.NewGuid();
                cashiersCheck.CreatedDate = datetime;
                cashiersCheck.UpdatedDate = datetime;
                cashiersCheck.UserID = usermanager.GetUserId(User);
                _context.Add(cashiersCheck);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    if (cashiersCheck.CatchReceiptID != null)
                    {
                        return RedirectToAction(nameof(Details), nameof(Data.CatchReceipts), new { id = cashiersCheck.CatchReceiptID });
                    }
                    else if (cashiersCheck.ReceiptID != null)
                    {
                        return RedirectToAction(nameof(Details), nameof(Data.Receipts), new { id = cashiersCheck.ReceiptID });
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(cashiersCheck);
        }

        // GET: CashiersChecks/Edit/5
        public async Task<IActionResult> Edit(Guid? id, Guid? CheckReceiptID, bool IsCatchReceipts)
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

            if (CheckReceiptID != null && CheckReceiptID != Guid.Empty)
            {
                if (IsCatchReceipts)
                {
                    cashiersCheck.CatchReceiptID = CheckReceiptID;
                }
                else
                {
                    cashiersCheck.ReceiptID = CheckReceiptID;
                }
            }
            return View(cashiersCheck);
        }

        // POST: CashiersChecks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,CatchReceiptID,ReceiptID,CheckNumber,AccountNumber,CodeBank,BankName,CodeBranch,BranchName,DateOfTheWorthy,AmountOfMoney,CreatedDate,UserID")] CashiersCheck cashiersCheck)
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
                    var result = await _context.SaveChangesAsync();
                    if (result > 0)
                    {
                        if (cashiersCheck.CatchReceiptID != null)
                        {
                            return RedirectToAction(nameof(Details), nameof(Data.CatchReceipts), new { id = cashiersCheck.CatchReceiptID });
                        }
                        else if (cashiersCheck.ReceiptID != null)
                        {
                            return RedirectToAction(nameof(Details), nameof(Data.Receipts), new { id = cashiersCheck.ReceiptID });
                        }
                    }
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
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                if (cashiersCheck.CatchReceiptID != null)
                {
                    return RedirectToAction(nameof(Details), nameof(Data.CatchReceipts), new { id = cashiersCheck.CatchReceiptID });
                }
                else if (cashiersCheck.ReceiptID != null)
                {
                    return RedirectToAction(nameof(Details), nameof(Data.Receipts), new { id = cashiersCheck.ReceiptID });
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CashiersCheckExists(Guid id)
        {
            return _context.CashiersCheck.Any(e => e.ID == id);
        }
    }
}
