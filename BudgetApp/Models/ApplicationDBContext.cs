using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Models
{
  public class ApplicationDbContext:DbContext
  {
    public ApplicationDbContext(DbContextOptions options):base(options)
    {}
    
    public DbSet<Transaction> Transactions { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    
      // Configure the relationship between User and Transaction
      modelBuilder.Entity<Transaction>()
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Cascade);
        
      // Add seed data for predefined users
      modelBuilder.Entity<User>().HasData(
        // new User { UserId = 0, Name = "Default User", ProfileImagePath = "profile.jpg" },
        new User { UserId = 1, Name = "Jamal Patel", ProfileImagePath = "JamalPatel.png" },
        new User { UserId = 2, Name = "Amina Hassan", ProfileImagePath = "AminaHassan.png" },
        new User { UserId = 3, Name = "Kenji Tanaka", ProfileImagePath = "KenjiTanaka.png" },
        new User { UserId = 4, Name = "Maria Garcia", ProfileImagePath = "MariaGarcia.png" },
        new User { UserId = 5, Name = "Gerard Haversham", ProfileImagePath = "GerardHaversham.png" }
      );
    }

  }
 
}
