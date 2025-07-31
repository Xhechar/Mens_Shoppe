
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("order/[controller]")]
public class OrderController : ControllerBase
{

  private readonly IOrderRepository orderRepository;
  public OrderController(IOrderRepository orderRepository)
  {
    this.orderRepository = orderRepository;
  }

  [HttpPost("create-order")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 404)]
  public IActionResult CreateOrder()
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Order>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.orderRepository.CreateOrder(userId);
    
    if (!result.Success)
    {
      return BadRequest(result);
    }

    return Ok(result);
  }

  [HttpGet("get-order-by-id/{orderId}")]
  [Authorize(Roles = "User,Admin")]
  [ProducesResponseType(typeof(Order), 200)]
  [ProducesResponseType(typeof(object), 400)]
  [ProducesResponseType(typeof(object), 404)]
  public IActionResult GetOrderById(string orderId)
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Order>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.orderRepository.GetOrderById(orderId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-orders-by-user")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(List<Order>), 200)]
  [ProducesResponseType(typeof(object), 400)]
  [ProducesResponseType(typeof(object), 404)]
  public IActionResult GetOrdersByUser()
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Order>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.orderRepository.GetOrdersByUserId(userId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-all-orders")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(List<Order>), 200)]
  [ProducesResponseType(typeof(object), 400)]
  [ProducesResponseType(typeof(object), 404)]
  public IActionResult GetAllOrders()
  {
    var result = this.orderRepository.GetAllOrders();
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-completed-orders")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(List<Order>), 200)]
  [ProducesResponseType(typeof(object), 400)]
  [ProducesResponseType(typeof(object), 404)]
  public IActionResult GetCompletedOrders()
  {
    var result = this.orderRepository.GetCompletedOrders();
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-pending-orders")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(List<Order>), 200)]
  [ProducesResponseType(typeof(object), 400)]
  [ProducesResponseType(typeof(object), 404)]
  public IActionResult GetPendingOrders()
  {
    var result = this.orderRepository.GetPendingOrders();
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpPut("toggle-order-status/{orderId}")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 404)]
  public IActionResult ToggleOrderStatus(string orderId)
  {
    var result = this.orderRepository.ToggleOrderStatus(orderId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpDelete("delete-order/{orderId}")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Order>), 404)]
  public IActionResult DeleteOrder(string orderId)
  {
    var result = this.orderRepository.DeleteOrder(orderId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

}