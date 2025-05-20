using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Przychodnia.Features.Entities.AppointmentFeature.Models;
using Przychodnia.Features.Entities.ExaminationFeature.Models;
using Przychodnia.Features.Entities.LaboratoryFeature.Models;
using Przychodnia.Features.Entities.PatientFeature.Models;
using Przychodnia.Features.Entities.PostalCodeFeature.Models;
using Przychodnia.Features.Entities.UserFeature.Models;
using Przychodnia.Features.Entities.UserTypesFeature.Models;


namespace Przychodnia.Data
{
    internal class DBData
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public void fillDatabase(ModelBuilder modelBuilder)
        {
            //USER TYPES
            modelBuilder.Entity<UserType>().HasData(
            new UserType { Id = 1, Name = "Administrator" },
            new UserType { Id = 2, Name = "Lekarz" },
            new UserType { Id = 3, Name = "Laborant" },
            new UserType { Id = 4, Name = "Rejestrator" },
            new UserType { Id = 5, Name = "Kierownik laboratorium" });

            //USERS
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Login = "admin", PasswordHash = "admin", UserTypeId = 1, IsActive = true },
                                                new User { Id = 2, Login = "lekarz1", PasswordHash = "lekarz1", UserTypeId = 2, LicenseNumber = "123", FirstName = "Monika", LastName = "Kowalska", IsActive = true },
                                                new User { Id = 3, Login = "lekarz2", PasswordHash = "lekarz2", UserTypeId = 2, LicenseNumber = "456", FirstName = "Dariusz", LastName = "Kowalski", IsActive = true },
                                                new User { Id = 4, Login = "lekarz3", PasswordHash = "lekarz3", UserTypeId = 2, LicenseNumber = "789", FirstName = "Dorota", LastName = "Kowalska", IsActive = true },
                                                new User { Id = 5, Login = "lekarz4", PasswordHash = "lekarz4", UserTypeId = 2, LicenseNumber = "147", FirstName = "Marek", LastName = "Kowalski", IsActive = true },
                                                new User { Id = 6, Login = "rejestrator1", PasswordHash = "rejestrator1", FirstName = "Monika", LastName = "Nowak", UserTypeId = 4, IsActive = true },
                                                new User { Id = 7, Login = "rejestrator2", PasswordHash = "rejestrator2", FirstName = "Janina", LastName = "Nowak", UserTypeId = 4, IsActive = true },
                                                new User { Id = 8, Login = "kierownik1", PasswordHash = "kierownik1", FirstName = "Janina", LastName = "Nowak", LicenseNumber = "456", LaboratoryId = 1, UserTypeId = 5, IsActive = true },
                                                new User { Id = 9, Login = "kierownik2", PasswordHash = "kierownik2", FirstName = "Monika", LastName = "Nowak", LicenseNumber = "123", LaboratoryId = 2, UserTypeId = 5, IsActive = true },
                                                new User { Id = 10, Login = "laborant1", PasswordHash = "laborant1", FirstName = "Monika", LastName = "Kowalska", LicenseNumber = "456", LaboratoryId = 1, UserTypeId = 3, IsActive = true },
                                                new User { Id = 11, Login = "laborant2", PasswordHash = "laborant2", FirstName = "Mariusz", LastName = "Nowak", LicenseNumber = "789", LaboratoryId = 1, UserTypeId = 3, IsActive = true },
                                                new User { Id = 12, Login = "laborant3", PasswordHash = "laborant3", FirstName = "Dariusz", LastName = "Nowak", LicenseNumber = "123", LaboratoryId = 2, UserTypeId = 3, IsActive = true },
                                                new User { Id = 13, Login = "laborant4", PasswordHash = "laborant4", FirstName = "Robert", LastName = "Nowak", LicenseNumber = "147", LaboratoryId = 2, UserTypeId = 3, IsActive = true });
            //LABORATORIES
            modelBuilder.Entity<Laboratory>().HasData(new Laboratory { Id = 1, ManagerId = 8, Name = "Pracownia EKG", Type = "Kto wie" },
                                                      new Laboratory { Id = 2, ManagerId = 9, Name = "Pracownia", Type = "Kto wie" });

            //POSTAL CODES
            modelBuilder.Entity<PostalCode>().HasData(new PostalCode { Id = 1, City = "Gliwice", Code = "44-100" },
                                                      new PostalCode { Id = 2, City = "Katowice", Code = "44-001" },
                                                      new PostalCode { Id = 3, City = "Częstochowa", Code = "42-202" },
                                                      new PostalCode { Id = 4, City = "Jaworzno", Code = "43-600" },
                                                      new PostalCode { Id = 5, City = "Chorzów", Code = "41-500" },
                                                      new PostalCode { Id = 6, City = "Chybie", Code = "43-520" },
                                                      new PostalCode { Id = 7, City = "Łaziska Górne", Code = "43-170" },
                                                      new PostalCode { Id = 8, City = "Przyszowice", Code = "44-178" },
                                                      new PostalCode { Id = 9, City = "Orzesze", Code = "43-180" },
                                                      new PostalCode { Id = 10, City = "Bielsko-Biała", Code = "43-382" },
                                                      new PostalCode { Id = 11, City = "Zabrze", Code = "41-800" },
                                                      new PostalCode { Id = 12, City = "Bytom", Code = "41-902" },
                                                      new PostalCode { Id = 13, City = "Tychy", Code = "43-100" });
            

            //PATIENTS
            modelBuilder.Entity<Patient>().HasData(new Patient { Id = 1, Pesel = "82030512345", FirstName = "Anna", LastName = "Nowak", Sex = Sex.Female, PostalCodeId = 5, Street = "Dobra", HouseNumber = "5" },
                                                   new Patient { Id = 2, Pesel = "94071976543", FirstName = "Piotr", LastName = "Nowak", Sex = Sex.Male, PostalCodeId = 1, Street = "Nowa", HouseNumber = "5" },
                                                   new Patient { Id = 3, Pesel = "61051223456", FirstName = "Piort", LastName = "Wiśniewski", Sex = Sex.Male, PostalCodeId = 1, Street = "Jasna", HouseNumber = "5" },
                                                   new Patient { Id = 4, Pesel = "76032345678", FirstName = "Jan", LastName = "Nowak", Sex = Sex.Male, PostalCodeId = 11, Street = "Tęczowa", HouseNumber = "5" },
                                                   new Patient { Id = 5, Pesel = "89012598765", FirstName = "Robert", LastName = "Wiśniewski", Sex = Sex.Male, PostalCodeId = 11, Street = "Kwiatowa", HouseNumber = "5" },
                                                   new Patient { Id = 6, Pesel = "73051434567", FirstName = "Piotr", LastName = "Kowalski", Sex = Sex.Male, PostalCodeId = 1, Street = "Wakacyjna", HouseNumber = "5" },
                                                   new Patient { Id = 7, Pesel = "59080923412", FirstName = "Antoni", LastName = "Nowak", Sex = Sex.Male, PostalCodeId = 13, Street = "Spokojna", HouseNumber = "5" },
                                                   new Patient { Id = 8, Pesel = "68010176543", FirstName = "Anna", LastName = "Wiśniewska", Sex = Sex.Female, PostalCodeId = 7, Street = "Kamienna", HouseNumber = "5" },
                                                   new Patient { Id = 9, Pesel = "99072787654", FirstName = "Jan ", LastName = "Kowalski", Sex = Sex.Male, PostalCodeId = 5, Street = "Murarska", HouseNumber = "5" },
                                                   new Patient { Id = 10, Pesel = "81091554321", FirstName = "Aleksander", LastName = "Nowak", Sex = Sex.Male, PostalCodeId = 10, Street = "Zamkowa", HouseNumber = "5" },
                                                   new Patient { Id = 11, Pesel = "67061723456", FirstName = "Hanna", LastName = "Wiśniewska", Sex = Sex.Female, PostalCodeId = 1, Street = "Przyjazna", HouseNumber = "5" },
                                                   new Patient { Id = 12, Pesel = "85050198765", FirstName = "Hanna", LastName = "Nowak", Sex = Sex.Female, PostalCodeId = 1, Street = "Licealna", HouseNumber = "5" },
                                                   new Patient { Id = 13, Pesel = "72031254321", FirstName = "Beata", LastName = "Kowalska", Sex = Sex.Female, PostalCodeId = 2, Street = "Bagno", HouseNumber = "5" },
                                                   new Patient { Id = 14, Pesel = "95072987654", FirstName = "Anna", LastName = "Kowalczyk", Sex = Sex.Female, PostalCodeId = 6, Street = "Marszałkowska", HouseNumber = "5" },
                                                   new Patient { Id = 15, Pesel = "88040323412", FirstName = "Anna", LastName = "Kowalska", Sex = Sex.Female, PostalCodeId = 2, Street = "Taśmowa", HouseNumber = "5" });

            //APPOINTMENTS
            modelBuilder.Entity<Appointment>().HasData(new Appointment { Id = 1, Date = new DateTime(2025, 6, 2, 14, 30, 0), PatientId = 1, ScheduledById = 6, AttendingDoctorId = 3, Completed = false },
                                                       new Appointment { Id = 2, Date = new DateTime(2025, 6, 2, 15, 30, 0), PatientId = 2, ScheduledById = 6, AttendingDoctorId = 3, Completed = false },
                                                       new Appointment { Id = 3, Date = new DateTime(2025, 6, 2, 13, 30, 0), PatientId = 10, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                       new Appointment { Id = 4, Date = new DateTime(2025, 6, 2, 12, 30, 0), PatientId = 11, ScheduledById = 5, AttendingDoctorId = 2, Completed = false },
                                                       new Appointment { Id = 5, Date = new DateTime(2025, 6, 2, 14, 30, 0), PatientId = 9, ScheduledById = 5, AttendingDoctorId = 4, Completed = false },
                                                       new Appointment { Id = 6, Date = new DateTime(2025, 6, 2, 12, 30, 0), PatientId = 3, ScheduledById = 5, AttendingDoctorId = 3, Completed = false },
                                                       new Appointment { Id = 7, Date = new DateTime(2025, 5, 20, 11, 0, 0), PatientId = 7, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 8, Date = new DateTime(2025, 5, 20, 11, 30, 0), PatientId = 8, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 9, Date = new DateTime(2025, 5, 20, 12, 0, 0), PatientId = 9, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 10, Date = new DateTime(2025, 5, 20, 12, 30, 0), PatientId = 10, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 11, Date = new DateTime(2025, 5, 20, 13, 0, 0), PatientId = 11, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 12, Date = new DateTime(2025, 5, 20, 13, 30, 0), PatientId = 12, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 13, Date = new DateTime(2025, 5, 20, 14, 0, 0), PatientId = 13, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 14, Date = new DateTime(2025, 5, 20, 14, 30, 0), PatientId = 14, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 15, Date = new DateTime(2025, 5, 20, 15, 0, 0), PatientId = 15, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 16, Date = new DateTime(2025, 5, 20, 15, 30, 0), PatientId = 1, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 17, Date = new DateTime(2025, 5, 20, 16, 0, 0), PatientId = 2, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 18, Date = new DateTime(2025, 5, 21, 8, 0, 0), PatientId = 3, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 19, Date = new DateTime(2025, 5, 21, 8, 30, 0), PatientId = 4, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 20, Date = new DateTime(2025, 5, 21, 9, 0, 0), PatientId = 5, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 21, Date = new DateTime(2025, 5, 21, 9, 30, 0), PatientId = 6, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 22, Date = new DateTime(2025, 5, 21, 10, 0, 0), PatientId = 7, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 23, Date = new DateTime(2025, 5, 21, 10, 30, 0), PatientId = 8, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 24, Date = new DateTime(2025, 5, 21, 11, 0, 0), PatientId = 9, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 25, Date = new DateTime(2025, 5, 21, 11, 30, 0), PatientId = 10, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 26, Date = new DateTime(2025, 5, 21, 12, 0, 0), PatientId = 11, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 27, Date = new DateTime(2025, 5, 21, 12, 30, 0), PatientId = 12, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 28, Date = new DateTime(2025, 5, 21, 13, 0, 0), PatientId = 13, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 29, Date = new DateTime(2025, 5, 21, 13, 30, 0), PatientId = 14, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 30, Date = new DateTime(2025, 5, 21, 14, 0, 0), PatientId = 15, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 31, Date = new DateTime(2025, 5, 21, 14, 30, 0), PatientId = 1, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 32, Date = new DateTime(2025, 5, 21, 15, 0, 0), PatientId = 2, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 33, Date = new DateTime(2025, 5, 21, 15, 30, 0), PatientId = 3, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 34, Date = new DateTime(2025, 5, 21, 16, 0, 0), PatientId = 4, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 35, Date = new DateTime(2025, 5, 22, 8, 0, 0), PatientId = 5, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 36, Date = new DateTime(2025, 5, 22, 8, 30, 0), PatientId = 6, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 37, Date = new DateTime(2025, 5, 22, 9, 0, 0), PatientId = 7, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 38, Date = new DateTime(2025, 5, 22, 9, 30, 0), PatientId = 8, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 39, Date = new DateTime(2025, 5, 22, 10, 0, 0), PatientId = 9, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 40, Date = new DateTime(2025, 5, 22, 10, 30, 0), PatientId = 10, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 41, Date = new DateTime(2025, 5, 22, 11, 0, 0), PatientId = 11, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 42, Date = new DateTime(2025, 5, 22, 11, 30, 0), PatientId = 12, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 43, Date = new DateTime(2025, 5, 22, 12, 0, 0), PatientId = 13, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 44, Date = new DateTime(2025, 5, 22, 12, 30, 0), PatientId = 14, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 45, Date = new DateTime(2025, 5, 22, 13, 0, 0), PatientId = 15, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 46, Date = new DateTime(2025, 5, 22, 13, 30, 0), PatientId = 1, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 47, Date = new DateTime(2025, 5, 22, 14, 0, 0), PatientId = 2, ScheduledById = 6, AttendingDoctorId = 4, Completed = false },
                                                        new Appointment { Id = 48, Date = new DateTime(2025, 5, 22, 14, 30, 0), PatientId = 3, ScheduledById = 6, AttendingDoctorId = 5, Completed = false },
                                                        new Appointment { Id = 49, Date = new DateTime(2025, 5, 22, 15, 0, 0), PatientId = 4, ScheduledById = 7, AttendingDoctorId = 3, Completed = false },
                                                        new Appointment { Id = 50, Date = new DateTime(2025, 5, 22, 15, 30, 0), PatientId = 5, ScheduledById = 7, AttendingDoctorId = 2, Completed = false },
                                                        new Appointment { Id = 51, Date = new DateTime(2025, 5, 22, 16, 0, 0), PatientId = 6, ScheduledById = 6, AttendingDoctorId = 4, Completed = false });
            //EXAMINATIONS
            modelBuilder.Entity<Examination>().HasData(new Examination { Id = 1, ExaminationType = ExaminationType.Laboratory, OrderedById = 3, PerformingDoctorId = 3, PatientId = 1, PerformingLaboratoryId = 2});
        }
    }
}
