
using System.Security.Claims;

public static class JwtHelper
{
  public static string? GetUserIdFromToken(HttpContext context)
  {
    return context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
  }
}