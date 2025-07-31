
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("review/[controller]")]
public class ReviewController : ControllerBase
{
  private readonly IReviewRepository reviewRepository;

  public ReviewController(IReviewRepository reviewRepository)
  {
    this.reviewRepository = reviewRepository;
  }

  [HttpPost("create-review/{productId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 404)]
  public IActionResult CreateReview(string productId, [FromBody] CreateReviewDto createReviewDto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
      return BadRequest(new RepositoryResult<Review>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Review>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.reviewRepository.CreateReview(createReviewDto, userId, productId);
    
    if (!result.Success)
    {
      return BadRequest(result);
    }

    return Ok(result);
  }

  [HttpPut("update-review/{reviewId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 404)]
  public IActionResult UpdateReview(string reviewId, [FromBody] UpdateReviewDto updateReviewDto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
      return BadRequest(new RepositoryResult<Review>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Review>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.reviewRepository.UpdateReview(userId, reviewId, updateReviewDto);
    
    if (!result.Success)
    {
      return BadRequest(result);
    }

    return Ok(result);
  }

  [HttpDelete("delete-review/{reviewId}")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 404)]
  public IActionResult DeleteReview(string reviewId)
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<Review>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.reviewRepository.DeleteReview(userId, reviewId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-review-by-id/{reviewId}")]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 404)]
  public IActionResult GetReviewById(string reviewId)
  {
    var result = this.reviewRepository.GetReviewById(reviewId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-all-reviews")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Review>), 404)]
  public IActionResult GetAllReviews()
  {
    var result = this.reviewRepository.GetAllReviews();
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-reviews-by-product/{productId}")]
  [ProducesResponseType(typeof(RepositoryResult<List<Review>>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<List<Review>>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<List<Review>>), 404)]
  public IActionResult GetReviewsByProduct(string productId)
  {
    var result = this.reviewRepository.GetReviewsByProductId(productId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-reviews-by-user")]
  [Authorize(Roles = "User")]
  [ProducesResponseType(typeof(RepositoryResult<List<Review>>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<List<Review>>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<List<Review>>), 404)]
  public IActionResult GetReviewsByUser()
  {
    string? userId = JwtHelper.GetUserIdFromToken(HttpContext);
    if (userId == null)
    {
      return Unauthorized(new RepositoryResult<List<Review>>
      {
        Success = false,
        Message = "Unauthorized",
        Error = "User ID not found in token"
      });
    }

    var result = this.reviewRepository.GetReviewsByUserId(userId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }
}