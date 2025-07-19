
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
  public DataContext(DbContextOptions<DataContext> options) : base(options)
  {
  }

  public DbSet<User> user { get; set; }
  public DbSet<Product> product { get; set; }
  public DbSet<Review> review { get; set; }
  public DbSet<Order> order { get; set; }
  public DbSet<Cart> cart { get; set; }
  public DbSet<Favourite> favourite { get; set; }
  public DbSet<Recovery> recovery { get; set; }

  protected override void OnModelCreating(ModelBuilder mb)
  {
    base.OnModelCreating(mb);

    // Unique constraints
        mb.Entity<User>()
          .HasIndex(u => u.Email).IsUnique();
        mb.Entity<User>()
          .HasIndex(u => u.PhoneNumber).IsUnique();

        mb.Entity<Review>()
          .HasOne(r => r.Owner)
          .WithMany(u => u.UserReviews)
          .HasForeignKey(r => r.UserId)
          .OnDelete(DeleteBehavior.Cascade);

        mb.Entity<Review>()
          .HasOne(r => r.PurchasedProduct)
          .WithMany(p => p.ProductReviews)
          .HasForeignKey(r => r.ProductId)
          .OnDelete(DeleteBehavior.Cascade);

        mb.Entity<Order>()
          .HasOne(o => o.Owner)
          .WithMany(u => u.UserOrders)
          .HasForeignKey(o => o.UserId)
          .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Order>()
              .HasOne(o => o.PurchasedProduct)
              .WithMany(p => p.ProductOrders)
              .HasForeignKey(o => o.ProductId)
              .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Cart>()
              .HasOne(c => c.Owner)
              .WithMany(u => u.UserCarts)
              .HasForeignKey(c => c.UserId)
              .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Cart>()
              .HasOne(c => c.CartProduct)
              .WithMany(p => p.ProductCarts)
              .HasForeignKey(c => c.ProductId)
              .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Favourite>()
              .HasOne(f => f.Owner)
              .WithMany(u => u.UserFavourites)
              .HasForeignKey(f => f.UserId)
              .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Favourite>()
              .HasOne(f => f.FavouriteProduct)
              .WithMany(p => p.ProductFavourites)
              .HasForeignKey(f => f.ProductId)
              .OnDelete(DeleteBehavior.NoAction);

        mb.Entity<Recovery>()
              .HasOne(r => r.RecoveredUser)
              .WithMany(u => u.UserRecoveries)
              .HasForeignKey(r => r.UserId)
              .OnDelete(DeleteBehavior.NoAction);
  }
}