using Microsoft.EntityFrameworkCore;
using Przychodnia.Model;

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

        modelBuilder.Entity<UserType>().HasData(
            new UserType { Id = 1, Name = "Admin" },
            new UserType { Id = 2, Name = "Lekarz" },
            new UserType { Id = 3, Name = "Laborant" },
            new UserType { Id = 4, Name = "Rejestrator" },
            new UserType { Id = 5, Name = "Menadżer" },
            new UserType { Id = 6, Name = "Kierownik laboratorium" });

        modelBuilder.Entity<User>().HasData(new User { Id = 1, Login = "admin", PasswordHash = "admin", UserTypeId = 1 });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}