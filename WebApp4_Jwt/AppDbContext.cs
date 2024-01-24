using System.Collections.Generic;
using System.Reflection.Emit;
using WebApp4_Jwt.DataStore;
using Microsoft.EntityFrameworkCore;

namespace WebApp4_Jwt
{
    public class AppDbContext : DbContext
    {
        private static string _connectionString;
        public AppDbContext()
        {

        }

        public AppDbContext(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString)
                 .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Login).IsUnique();

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.HasOne(x => x.Role)
                    .WithMany(x => x.Users);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(x => x.RoleType);
                entity.HasIndex(x => x.Name).IsUnique();

            });
        }
    }
}
