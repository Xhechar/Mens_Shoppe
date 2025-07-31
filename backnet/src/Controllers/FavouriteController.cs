
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("favourite/[controller]")]
public class FavouriteController : ControllerBase
{
  private readonly IFavouriteRepository favouriteRepository;

  public FavouriteController(IFavouriteRepository favouriteRepository)
  {
    this.favouriteRepository = favouriteRepository;
  }

  [HttpPost("add-to-favourite/{productId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 404)]
  public IActionResult AddToFavourite(string productId)
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Favourite>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.favouriteRepository.AddToFavourite(userId, productId);
    
    return Ok(result);
  }

  [HttpDelete("remove-from-favourite/{favouriteId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 404)]
  public IActionResult RemoveFromFavourite(string favouriteId)
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Favourite>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.favouriteRepository.RemoveFromFavourite(userId, favouriteId);
    
    return Ok(result);
  }

  [HttpGet("get-favourite-items")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 404)]
  public IActionResult GetFavouriteItems()
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Favourite>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.favouriteRepository.GetFavouriteItems(userId);
    
    return Ok(result);
  }

  [HttpDelete("clear-favourites")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Favourite>), 404)]
  public IActionResult ClearFavourites()
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Favourite>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.favouriteRepository.ClearFavourites(userId);
    
    return Ok(result);
  }
}