
using System.ComponentModel.DataAnnotations;

public class Review
{
  [Key]
  [Required]
  public required string ReviewId { get; set; }
  [Required]
  public required string ProductId { get; set; }
  [Required]
  public required string UserId { get; set; }
  [Required]
  [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
  [MaxLength(5)]
  public int Rating { get; set; }
  [Required]
  [MaxLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
  [MinLength(10, ErrorMessage = "Comment must be at least 1 character long.")]
  public required string Comment { get; set; }
  public bool IsDeleted { get; set; } = false;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public Product PurchasedProduct { get; set; }
  public User Owner { get; set; }
}