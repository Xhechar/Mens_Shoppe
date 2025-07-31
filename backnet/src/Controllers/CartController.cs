
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("cart/[controller]")]
public class CartController : ControllerBase
{

  private readonly ICartRepository cartRepository;

  public CartController(ICartRepository cartRepository)
  {
    this.cartRepository = cartRepository;
  }

  [HttpPost("add-to-cart/{productId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 404)]
  public IActionResult AddToCart(string productId, [FromBody] CreateCartDto createCartDto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
      return BadRequest(new RepositoryResult<Cart>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Cart>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.cartRepository.AddToCart(userId, productId, createCartDto);
    
    return Ok(result);
  }

  [HttpDelete("remove-from-cart/{cartId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 404)]
  public IActionResult RemoveFromCart(string cartId)
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Cart>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.cartRepository.RemoveFromCart(userId, cartId);
    
    return Ok(result);
  }

  [HttpGet("get-cart-items")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 404)]
  public IActionResult GetCartItems()
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Cart>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.cartRepository.GetCartItems(userId);
    
    return Ok(result);
  }

  [HttpDelete("clear-cart")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 404)]
  public IActionResult ClearCart()
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Cart>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.cartRepository.ClearCart(userId);
    
    return Ok(result);
  }

  [HttpPut("increment-cart-item/{cartId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 404)]
  public IActionResult IncrementCartItem(string cartId)
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Cart>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.cartRepository.IncrementCartItem(cartId, userId);
    
    return Ok(result);
  }

  [HttpPut("decrement-cart-item/{cartId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Cart>), 404)]
  public IActionResult DecrementCartItem(string cartId)
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Cart>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.cartRepository.DecrementCartItem(cartId, userId);
    
    return Ok(result);
  }
}