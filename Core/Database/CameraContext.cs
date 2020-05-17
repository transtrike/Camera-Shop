using Camera_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Camera_Shop.Database
{
     public class CameraContext : DbContext
     {
          public DbSet<Camera> Cameras { get; set; }
          
          
          
          protected override void OnModelCreating(ModelBuilder modelBuilder) => 
               modelBuilder.Entity<Camera>().HasKey("Id");
     }
}