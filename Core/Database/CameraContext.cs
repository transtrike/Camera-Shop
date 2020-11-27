using Data.Models.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Camera_Shop.Database
{
	public class CameraContext : IdentityDbContext<User, Role, int>
	{
		public DbSet<Camera> Cameras { get; set; }

		public DbSet<Brand> Brands { get; set; }
		
		public CameraContext(DbContextOptions options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Camera>()
				.HasKey(key => key.Id);

			modelBuilder.Entity<Brand>()
				.HasKey(key => key.Id);

			base.OnModelCreating(modelBuilder);
		}
	}
}