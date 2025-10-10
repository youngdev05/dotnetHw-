using HW4.Models;
using Microsoft.EntityFrameworkCore;

namespace HW4.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed initial data
        modelBuilder.Entity<User>().HasData(
            new User { 
                Id = 1, 
                Username = "user1", 
                Password = "user", 
                Gender = "Male", 
                CreatedDate = new DateTime(2024, 5, 1), 
                UpdatedDate = new DateTime(2024, 5, 1) 
            },
            new User { 
                Id = 2, 
                Username = "maria", 
                Password = "123", 
                Gender = "Female", 
                CreatedDate = new DateTime(2024, 5, 3), 
                UpdatedDate = new DateTime(2024, 5, 3) 
            },
            new User { 
                Id = 3, 
                Username = "alex", 
                Password = "123", 
                Gender = "Male", 
                CreatedDate = new DateTime(2024, 6, 10), 
                UpdatedDate = new DateTime(2024, 6, 10) 
            },
            new User { 
                Id = 4, 
                Username = "olga", 
                Password = "123", 
                Gender = "Female", 
                CreatedDate = new DateTime(2024, 7, 1), 
                UpdatedDate = new DateTime(2024, 7, 1) 
            },
            new User { 
                Id = 5, 
                Username = "artem", 
                Password = "123", 
                Gender = "Male", 
                CreatedDate = new DateTime(2024, 8, 20), 
                UpdatedDate = new DateTime(2024, 8, 20) 
            }
        );
    }
}