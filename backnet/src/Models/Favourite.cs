
using System.ComponentModel.DataAnnotations;

public class Favourite
{
  [Key]
  [Required]
  public required string FavouriteId { get; set; }
  [Required]
  public required string ProductId { get; set; }
  [Required]
  public required string UserId { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public User Owner { get; set; }
  public Product FavouriteProduct { get; set; }
}