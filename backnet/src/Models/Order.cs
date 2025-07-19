
using System.ComponentModel.DataAnnotations;

public class Order
{
  [Key]
  [Required]
  public required string OrderId { get; set; }
  [Required]
  public required string ProductId { get; set; }
  [Required]
  public required string UserId { get; set; }
  [Required]
  [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
  public int Quantity { get; set; }
  public decimal TotalPrice { get; set; }
  public bool IsOrderCompleted { get; set; } = false;
  public bool IsDeleted { get; set; } = false;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public Product PurchasedProduct { get; set; }
  public User Owner { get; set; }
}