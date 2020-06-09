using Camera_Shop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Camera_Shop.Database
{
     public class CameraContext : IdentityDbContext
     {
          public DbSet<Camera> Cameras { get; set; }
          public DbSet<User> Users { get; set; }
          
          public CameraContext(DbContextOptions options)
               : base(options) { }
          
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               modelBuilder.Entity<Camera>()
                    .HasKey(key => key.Id);
               
               base.OnModelCreating(modelBuilder);
          }
     }
}