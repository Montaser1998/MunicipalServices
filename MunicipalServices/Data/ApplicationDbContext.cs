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
        public DbSet<MunicipalServices.Data.CashiersCheck> CashiersCheck { get; set; }
        public DbSet<MunicipalServices.Data.VacationRequest> VacationRequest { get; set; }
        public DbSet<MunicipalServices.Data.ComplaintForm> ComplaintForm { get; set; }
        public DbSet<MunicipalServices.Data.CraftAndIndustryLicense> CraftAndIndustryLicense { get; set; }
        public DbSet<MunicipalServices.Data.WaterMeterSubscriptionRequest> WaterMeterSubscriptionRequest { get; set; }
    }
}
