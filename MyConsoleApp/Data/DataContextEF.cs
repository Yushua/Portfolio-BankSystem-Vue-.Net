using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyApp.Models;

namespace MyApp.Data
{
    public class DataContextEF : DbContext
    {
        private IConfiguration _config;
        public DataContextEF(IConfiguration config){
            _config = config;
        }
        
        public DbSet<Computer> Computers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    options => options.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set the default schema
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            // Map the entity to the correct table
            modelBuilder.Entity<Computer>()
                .ToTable("Computer") // Ensure this matches the table name in your database
                .HasKey(c => c.ComputerId); // Ensure this matches the primary key property
        }
    }
}
