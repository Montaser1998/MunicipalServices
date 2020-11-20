using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalServices.Component
{

    [ViewComponent(Name = "CashiersChecksList")]
    public class CashiersChecksComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public CashiersChecksComponent(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(Guid id, bool IsCatchReceipts)
        {
            var items = await GetItemsAsync(id, IsCatchReceipts);
            return View(items);
        }
        private async Task<List<Data.CashiersCheck>> GetItemsAsync(Guid id, bool IsCatchReceipts)
        {
            var model = db.CashiersCheck.Include(u => u.User).AsQueryable();
            if (IsCatchReceipts)
            {
                model = model.Where(c => c.Deleted == false && c.CatchReceiptID == id);
            }
            else
            {
                model = model.Where(c => c.Deleted == false && c.ReceiptID == id);
            }
            return await model.OrderBy(c=>c.CreatedDate).ToListAsync();
        }
    }

}
