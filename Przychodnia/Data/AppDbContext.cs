using Microsoft.EntityFrameworkCore;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.ExaminationFeature.Models;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Models;

namespace Przychodnia.Data;

public class AppDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Examination> Examinations { get; set; }
    public DbSet<Laboratory> Laboratories { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public DbSet<PostalCode> PostalCodes { get; set; }

    private string DbPath { get; }
    public AppDbContext()
    {
        DbPath = System.IO.Path.Combine(AppContext.BaseDirectory, "database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Laboratory>()
            .HasOne(l => l.Manager)
            .WithOne(u => u.ManagedLaboratory)
            .HasForeignKey<Laboratory>(l => l.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Laboratory)
            .WithMany(l => l.Workers)
            .HasForeignKey(u => u.LaboratoryId)
            .OnDelete(DeleteBehavior.SetNull);

        DBData dBData = new DBData();
        dBData.fillDatabase(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}