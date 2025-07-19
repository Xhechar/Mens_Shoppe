
using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
  public string Name { get; set; }
  public string Email { get; set; }
  public string Country { get; set; }
  public string PhoneNumber { get; set; }
  public string Password { get; set; }
}