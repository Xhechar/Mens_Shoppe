
using System.ComponentModel.DataAnnotations;

public class Product
{
  [Key]
  [Required]
  public required string ProductId { get; set; }
  [Required]
  [MaxLength(100)]
  public required string Name { get; set; }
  [Required]
  [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive value.")]
  public int Price { get; set; }
  [MaxLength(1000)]
  [Required]
  public required string Description { get; set; }
  [Required]
  [MaxLength(50)]
  public required string Type { get; set; }
  [Required]
  [MaxLength(50)]
  public required string Size { get; set; }
  [Required]
  [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
  public int Quantity { get; set; }
  [Required]
  [Range(0, int.MaxValue, ErrorMessage = "Stock limit must be a non-negative value.")]
  public int StockLimit { get; set; }
  [Required]
  public required string Images { get; set; }
  [Required]
  public bool IsDeleted { get; set; } = false;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public ICollection<Review> ProductReviews { get; set; }
  public ICollection<Order> ProductOrders { get; set; }
  public ICollection<Cart> ProductCarts { get; set; }
  public ICollection<Favourite> ProductFavourites { get; set; }
}