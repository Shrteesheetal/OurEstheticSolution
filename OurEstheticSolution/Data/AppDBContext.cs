using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurEstheticSolution.Models;
using OurEstheticSolution.Models.Entities;
using OurEstheticSolution.ViewModel;


namespace OurEstheticSolution.Data
{
    public class AppDBContext : IdentityDbContext<User>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
         // Consolidated OnModelCreating method to avoid duplicate definitions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example Fluent configuration if needed
            // modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();
        }

    }
}


    
