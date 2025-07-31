
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IAuthRepository authRepository;

  public AuthController(IAuthRepository authRepository)
  {
    this.authRepository = authRepository;
  }

  [HttpPost("login")]
  [ProducesResponseType(typeof(RepositoryResult<object>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<object>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<object>), 404)]
  public IActionResult Login([FromBody] LoginDto loginDto)
  {
    if(!ModelState.IsValid) 
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

      return BadRequest(new RepositoryResult<RepositoryResult<object>>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    var result = authRepository.Login(loginDto);
    
    if (result.Success)
    {
      if(result.Token == null) return Unauthorized(new RepositoryResult<RepositoryResult<object>>
      {
        Success = false,
        Message = "Login failed",
        Error = "Token generation failed"
      });

      HttpContext.Response.Cookies.Append("access_token", result.Token, new CookieOptions
      {
        HttpOnly = true,
        Secure = false,
        SameSite = SameSiteMode.Strict,
        Expires = DateTimeOffset.UtcNow.AddHours(1)
      });

      return Ok(result);
    }

    return Ok(result);
  }

  [HttpPost("reset-password")]
  [ProducesResponseType(typeof(RepositoryResult<object>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<object>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<object>), 404)]
  public IActionResult ResetPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
  {
    if(!ModelState.IsValid) 
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

      return BadRequest(new RepositoryResult<RepositoryResult<object>>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    var result = authRepository.ResetPassword(forgotPasswordDto);

    return Ok(result);
  }
  
  [HttpPost("get-recovery-code/{email}")]
  [ProducesResponseType(typeof(RepositoryResult<object>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<object>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<object>), 404)]
  public IActionResult GetRecoveryCode(string email)
  {
    if (string.IsNullOrEmpty(email))
    {
      return BadRequest(new RepositoryResult<RepositoryResult<object>>
      {
        Success = false,
        Message = "Email Reqired",
        Error = "Email cannot be null or empty"
      });
    }

    var result = authRepository.GetRecoveryCode(email);

    return Ok(result);
  }

  [HttpPost("Logout")]
  [ProducesResponseType(typeof(RepositoryResult<object>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<object>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<object>), 404)]
  public IActionResult Logout()
  {
    HttpContext.Response.Cookies.Delete("access_token");

    return Ok(new RepositoryResult<RepositoryResult<object>>
    {
      Success = true,
      Message = "Logged out successfully"
    });
  }
}