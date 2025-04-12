using Microsoft.EntityFrameworkCore;
using Przychodnia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Data
{
    class MyAppContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        //public DbSet<ManagementPeriod> ManagementPeriods { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        private string DbPath { get; }
        public MyAppContext()
        {
            DbPath = "database.db";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ManagementPeriod>()
                .HasKey(mp => new { mp.ManagerId, mp.LaboratoryId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}