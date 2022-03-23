using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CS872_WebApp.Models;
using System.Threading.Tasks;

#nullable disable

namespace CS872_WebApp.Models
{
    public partial class ModelContext : DbContext
    {
        public DbSet<StandardViewModel> StandardViewModel { get; set; }

        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=dbcsproj.ccd5phonjhwq.us-east-1.rds.amazonaws.com;database=CS872Proj;uid=admin;pwd=Oseyi1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.27-mysql"));
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
