
using System.ComponentModel.DataAnnotations;

public class Recovery
{
    [Key]
    [Required]
    public string RecoveryId { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [Range(100000, 999999, ErrorMessage = "Recovery code must be a 6-digit number.")]
    public int RecoveryCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; }
    public User RecoveredUser { get; set; }
}