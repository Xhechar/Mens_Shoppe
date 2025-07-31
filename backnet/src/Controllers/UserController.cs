
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("user/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserRepository userRepository;

  public UserController(IUserRepository userRepository)
  {
    this.userRepository = userRepository;
  }

  [HttpPost("register")]
  [ProducesResponseType(typeof(RepositoryResult<User>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<User>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<User>), 404)]
  public IActionResult Register([FromBody] CreateUserDto userDto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
      return BadRequest(new RepositoryResult<User>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    var result = userRepository.Register(userDto);
    
    if (!result.Success)
    {
      return BadRequest(result);
    }

    return Ok(result);
  }

  [HttpPut("update-user")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<User>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<User>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<User>), 404)]
  public IActionResult UpdateUser([FromBody] UpdateUserDto updateUserDto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
      return BadRequest(new RepositoryResult<User>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<User>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = userRepository.UpdateUser(userId, updateUserDto);
    
    if (!result.Success)
    {
      return BadRequest(result);
    }

    return Ok(result);
  }

  [HttpGet("get-user}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<User>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<User>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<User>), 404)]
  public IActionResult GetUser()
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<User>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = userRepository.GetUserById(userId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-all-users")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(RepositoryResult<User>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<User>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<User>), 404)]
  public IActionResult GetAllUsers()
  {
    var result = userRepository.GetAllUsers();
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }
}