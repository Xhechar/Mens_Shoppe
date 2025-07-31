
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("product/[controller]")]
public class ProductController : ControllerBase
{
  private readonly IProductRepository productRepository;

  public ProductController(IProductRepository productRepository)
  {
    this.productRepository = productRepository;
  }

  [HttpPost("create-product")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 404)]
  public IActionResult CreateProduct([FromBody] CreateProductDto createProductDto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
      return BadRequest(new RepositoryResult<Product>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    var result = productRepository.CreateProduct(createProductDto);
    
    if (!result.Success)
    {
      return BadRequest(result);
    }

    return Ok(result);
  }

  [HttpPut("update-product/{productId}")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 404)]
  public IActionResult UpdateProduct(string productId, [FromBody] UpdateProductDto updateProductDto)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
      return BadRequest(new RepositoryResult<Product>
      {
        Success = false,
        Message = "Validation failed",
        Error = errors.ToList()[0]
      });
    }

    var result = productRepository.UpdateProduct(productId, updateProductDto);
    
    if (!result.Success)
    {
      return BadRequest(result);
    }

    return Ok(result);
  }

  [HttpDelete("delete-product/{productId}")]
  [Authorize(Roles = "Admin")]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 404)]
  public IActionResult DeleteProduct(string productId)
  {
    var result = productRepository.DeleteProduct(productId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-product/{productId}")]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 404)]
  public IActionResult GetProduct(string productId)
  {
    var result = productRepository.GetProductById(productId);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-all-products")]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 404)]
  public IActionResult GetAllProducts()
  {
    var result = productRepository.GetAllProducts();
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("get-products-by-type/{type}")]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<RepositoryResult<Product>>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<RepositoryResult<Product>>), 404)]
  public IActionResult GetProductsByType(string type)
  {
    var result = productRepository.GetProductsByType(type);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }

  [HttpGet("search-products/?query")]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 200)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 400)]
  [ProducesResponseType(typeof(RepositoryResult<Product>), 404)]
  public IActionResult SearchProducts([FromQuery] string query)
  {
    if (string.IsNullOrEmpty(query))
    {
      return BadRequest(new RepositoryResult<List<Product>>
      {
        Success = false,
        Message = "Query is required",
        Error = "Search query cannot be null or empty"
      });
    }

    var result = productRepository.SearchProducts(query);
    
    if (!result.Success)
    {
      return NotFound(result);
    }

    return Ok(result);
  }
}