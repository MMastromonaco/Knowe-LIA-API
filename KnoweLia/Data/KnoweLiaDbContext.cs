using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using KnoweLia.Models;

namespace KnoweLia.Data
{
	public class KnoweLiaDbContext : DbContext
	{
		public KnoweLiaDbContext(DbContextOptions<KnoweLiaDbContext> options)
		: base(options)
		{ 
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<UserGroup> UserGroups { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserGroup>()
				.HasKey(ug => new { ug.UserId, ug.GroupId });

			modelBuilder.Entity<UserGroup>()
				.HasOne(ug => ug.User)
				.WithMany(u => u.UserGroups)
				.HasForeignKey(ug => ug.UserId);

			modelBuilder.Entity<UserGroup>()
				.HasOne(ug => ug.Group)
				.WithMany(g => g.UserGroups)
				.HasForeignKey(ug => ug.GroupId);
		}
	}
}
