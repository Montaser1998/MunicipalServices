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
        public IViewComponentResult InvokeAsync(Guid id, bool IsCatchReceipts)
        {
            var items = GetItemsAsync(id, IsCatchReceipts);
            return View(items);
        }
        private IQueryable<Data.CashiersCheck> GetItemsAsync(Guid id, bool IsCatchReceipts)
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
            return model.OrderBy(c=>c.CreatedDate);
        }
    }

}
