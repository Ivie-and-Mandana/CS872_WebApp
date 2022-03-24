using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS872_WebApp.Models;

namespace CS872_WebApp.Models
{
    public class BillDataContext : DbContext
    {
        public BillDataContext()
        { }

        public BillDataContext(DbContextOptions<BillDataContext> options)
            : base(options)
        {
        }

        public DbSet<BillViewModel> BillViewModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=dbcsproj.ccd5phonjhwq.us-east-1.rds.amazonaws.com;uid=admin;pwd=Oseyi1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreating(modelBuilder);

        }

       // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}