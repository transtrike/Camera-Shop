using System;
using Camera_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Camera_Shop.Database
{
     public class CameraContext : DbContext
     {
          public DbSet<Camera> Cameras { get; set; }

          public CameraContext() { }
          
          public static readonly Type[] AllowedSqlTypes = {
               typeof(string),
               typeof(int),
               typeof(uint),
               typeof(long),
               typeof(ulong),
               typeof(decimal),
               typeof(bool),
               typeof(DateTime)
          };
          
          protected override void OnModelCreating(ModelBuilder modelBuilder) => 
               modelBuilder.Entity<Camera>().HasKey("Id");
     }
}