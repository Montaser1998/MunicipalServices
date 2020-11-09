using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MunicipalServices.Data;

namespace MunicipalServices.Data
{
    public class ApplicationDbContext : IdentityDbContext<Data.Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MunicipalServices.Data.CatchReceipts> CatchReceipts { get; set; }
        public DbSet<MunicipalServices.Data.Receipts> Receipts { get; set; }
    }
}
