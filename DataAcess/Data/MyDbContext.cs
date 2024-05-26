using DataAcess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            // optionsBuilder.UseInMemoryDatabase("MyDatabase");
            //JOSED\SQLEXPRESS

            optionsBuilder.UseSqlServer("Server=JOSED\\SQLEXPRESS;Database=ClinicaDB;Trusted_Connection=True; MultipleActiveResultSets=true;TrustServerCertificate=True");
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Clinic> Clinics { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Appointments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired(true);

            modelBuilder.Entity<Clinic>()
                .HasMany(e => e.Appointments)
                .WithOne(e => e.Clinic)
                .HasForeignKey(e => e.ClinicId)
                .IsRequired(true);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "ADMIN" },
                new Role { Id = 2, Name = "USER" }); 

        }
    }
}