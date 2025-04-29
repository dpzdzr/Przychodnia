using Microsoft.EntityFrameworkCore;
using Przychodnia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Data
{
    class AppDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Laboratory> Laboratories { get; set; }
        public DbSet<ManagementPeriod> ManagementPeriods { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }

        private string DbPath { get; }
        public AppDbContext()
        {
            DbPath = "database.db";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ManagementPeriod>()
                .HasKey(mp => new { mp.ManagerId, mp.LaboratoryId });

            modelBuilder.Entity<UserType>().HasData(new UserType { Id = 1, Name = "Admin" });
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Login = "admin", PasswordHash="admin", UserTypeId = 1 });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}