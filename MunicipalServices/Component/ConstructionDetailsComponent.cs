using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MunicipalServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MunicipalServices.Component
{

    [ViewComponent(Name = "ConstructionDetailsList")]
    public class ConstructionDetailsComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;

        public ConstructionDetailsComponent(ApplicationDbContext context)
        {
            db = context;
        }
        public IViewComponentResult InvokeAsync(Guid id)
        {
            var items = GetItemsAsync(id);
            return View(items);
        }
        private IQueryable<Data.ConstructionDetails> GetItemsAsync(Guid id)
        {
            var model = db.ConstructionDetails.Include(u => u.User).Where(c=>c.ConstructionLicenseID == id).OrderBy(c => c.CreatedDate);
            return model;
        }
    }

}
