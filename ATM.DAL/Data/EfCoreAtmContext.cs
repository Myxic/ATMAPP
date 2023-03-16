using System;
using ATM.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ATM.DAL.Data
{
    public class AtmDbContext : DbContext
    {
        public AtmDbContext()
        {
        }

        public AtmDbContext(DbContextOptions<AtmDbContext> options):base(options)
        {

        }

        public DbSet<Admin> Admins { get; set; } 

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Workflow> Workflows { get; set; } 

        public DbSet<TransationRecords> Transations { get; set; }

        public DbSet<Complains> Complains { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    var ConnString = ConfigurationManager.ConnectionStrings["ATMDatabase"].ConnectionString;

        //    optionsBuilder.UseSqlServer(ConnString);


        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(e =>
            {
                e.Property(p => p.Id)
                    .IsRequired();
                e.HasIndex(p => p.Id)
                    .IsUnique();
            });


           
            modelBuilder.Entity<Customer>(e =>
            {
                e.Property(p => p.Id)
                    .IsRequired();
                e.HasIndex(p => p.Id)
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
