using Microsoft.EntityFrameworkCore;
using WebApp2_Magazin.Models;

namespace WebApp2_Magazin
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;
        public AppDbContext()
        {

        }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        /*
         dotnet ef migrations add InitialCreate --context AppDbContext
         dotnet ef database update
         dotnet ef database update --connection 'Host=localhost;Username=root;Password=secret;Database=postgres'
        */
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<StorageEntity> Storages { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Price).IsRequired();

                entity.HasOne(x => x.Category)
                    .WithMany(x => x.Products);

                entity.HasOne(x => x.Storage)
                    .WithMany(x => x.Products);
            });

            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsRequired();
            });

            modelBuilder.Entity<StorageEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsRequired();
            });
        }
    }
}
