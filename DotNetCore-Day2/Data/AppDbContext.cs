using DotNetCore_Day2.Model.Entities;
using DotNetCore_Day2.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore_Day2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                        .Property(p => p.Name)
                        .IsRequired();

            modelBuilder.Entity<Product>()
                        .Property(p => p.Price)
                        .HasColumnType("decimal(8,2)");

            modelBuilder.Entity<Product>()
                        .HasData(
                new Product() {Id=1, Name="Shirt",Price=500,Category=(Category)0},
                new Product() {Id=2, Name="Mouse",Price=300, Category = (Category)1 },
                new Product() { Id = 3, Name ="Keyboard",Price=700, Category =(Category) 1 },
                new Product() { Id = 4, Name ="Shoes",Price=1000, Category = (Category)0 },
                new Product() { Id = 5, Name ="Jeans",Price=1500, Category =(Category) 0},
                new Product() { Id = 6, Name ="Earphone",Price=600, Category = (Category)1},
                new Product() { Id = 7, Name ="Ball",Price=60, Category = (Category)2 }
                );
        }
    }
}
