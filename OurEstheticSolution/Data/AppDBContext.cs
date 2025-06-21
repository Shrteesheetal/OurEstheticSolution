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
        public DbSet<OurEstheticSolution.ViewModel.EmployeeViewModel> EmployeeViewModel { get; set; } = default!;
        public DbSet<Product> Products { get; set; }
        public DbSet<OurEstheticSolution.ViewModel.ProductViewModel> ProductViewModel { get; set; } = default!;

        // Consolidated OnModelCreating method to avoid duplicate definitions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example Fluent configuration if needed
            // modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();
        }

    }
}


    
