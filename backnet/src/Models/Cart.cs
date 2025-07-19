
using System.ComponentModel.DataAnnotations;

public class Cart 
{
  [Key]
  [Required]
  public required string CartId { get; set; }
  [Required]
  public required string UserId { get; set; }
  [Required]
  public required string ProductId { get; set; }
  [Required]
  [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
  public int Quantity { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public Product CartProduct { get; set; }
  public User Owner { get; set; }
}