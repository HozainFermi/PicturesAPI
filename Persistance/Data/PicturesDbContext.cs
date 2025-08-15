using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistance.Data
{
    public class PicturesDbContext: DbContext
    {
        public PicturesDbContext(DbContextOptions<PicturesDbContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }

        }


        public DbSet<AuctionEntity> Auctions { get; set; } = null!;
        public DbSet<CartEntity> Carts { get; set; } = null!;
        public DbSet<CartItemEntity> CartItems { get; set; } = null!;
        public DbSet<OrderEntity> Orders { get; set; } = null!;
        public DbSet<ProductEntity> Products { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        
        //TODO
        //public DbSet<RaffleEntity> Raffles { get; set; } = null!; 
       // public DbSet<CommentEntity> Comments { get; set; } = null!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //}
    }
}
