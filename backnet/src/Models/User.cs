
using System.ComponentModel.DataAnnotations;

public class User 
{
  [Key]
  [Required]
  public required string UserId { get; set; }
  [Required]
  public required string Name { get; set; }
  [Required]
  [EmailAddress]
  public required string Email { get; set; }
  [Required]
  public required string PhoneNumber { get; set; }
  [Required]
  public required string Country { get; set; }
  [Required]
  [MinLength(8)]
  public required string Password { get; set; }
  [Required]
  [MaxLength(10)]
  public string Role { get; set; } = "user";
  [Required]
  public bool IsWelcomed { get; set; } = true;
  [Required]
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  [Required]
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public ICollection<Review> UserReviews { get; set; }
  public ICollection<Order> UserOrders { get; set; }
  public ICollection<Cart> UserCarts { get; set; }
  public ICollection<Favourite> UserFavourites { get; set; }
  public ICollection<Recovery> UserRecoveries { get; set; }

}