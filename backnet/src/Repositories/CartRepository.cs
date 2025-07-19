
using Microsoft.EntityFrameworkCore;

public class CartRepository : ICartRepository 
{
  private readonly DataContext dataContext;

  public CartRepository(DataContext dataContext)
  {
    this.dataContext = dataContext;
  }

  public RepositoryResult<Cart> AddToCart(string UserId, string ProductId, CreateCartDto createCartDto)
  {
    if (string.IsNullOrWhiteSpace(UserId))
    {
      return RepositoryResponse.Failure<Cart>("User ID is invalid.");
    }

    if (string.IsNullOrWhiteSpace(ProductId))
    {
      return RepositoryResponse.Failure<Cart>("Product ID is invalid.");
    }

    User? user = this.dataContext.user.FirstOrDefault(u => u.UserId == UserId);
    if (user == null)
    {
      return RepositoryResponse.Failure<Cart>("User not found.");
    }

    Product? product = this.dataContext.product.FirstOrDefault(p => p.ProductId == ProductId);
    if (product == null)
    {
      return RepositoryResponse.Failure<Cart>("Product not found.");
    }

    Cart cart = new Cart
    {
      CartId = Guid.NewGuid().ToString(),
      UserId = UserId,
      ProductId = ProductId,
      Quantity = createCartDto.Quantity
    };

    this.dataContext.cart.Add(cart);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Cart>("Unable to add to cart at the moment.");
    }

    return RepositoryResponse.Success<Cart>("Product added to cart successfully", cart);
  }

  public RepositoryResult<Cart> RemoveFromCart(string UserId, string CartId)
  {
    if (string.IsNullOrWhiteSpace(UserId))
    {
      return RepositoryResponse.Failure<Cart>("User ID is invalid.");
    }

    if (string.IsNullOrWhiteSpace(CartId))
    {
      return RepositoryResponse.Failure<Cart>("Cart ID is invalid.");
    }

    Cart? cart = this.dataContext.cart.FirstOrDefault(c => c.UserId == UserId && c.CartId == CartId);
    if (cart == null)
    {
      return RepositoryResponse.Failure<Cart>("Cart item not found.");
    }

    this.dataContext.cart.Remove(cart);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Cart>("Unable to remove from cart at the moment.");
    }

    return RepositoryResponse.Success<Cart>("Cart item removed successfully", cart);
  }

  public RepositoryResult<Cart> GetCartItems(string UserId)
  {
    if (string.IsNullOrWhiteSpace(UserId))
    {
      return RepositoryResponse.Failure<Cart>("User ID is invalid.");
    }

    var cartItems = this.dataContext.cart.Include(c => c.Owner).Include(c => c.CartProduct).Where(c => c.UserId == UserId).ToList();

    if (cartItems.Count == 0)
    {
      return RepositoryResponse.Failure<Cart>("No items found in the cart.");
    }

    return RepositoryResponse.Success<Cart>("Cart items retrieved successfully", dataList: cartItems);
  }

  public RepositoryResult<Cart> ClearCart(string UserId)
  {
    if (string.IsNullOrWhiteSpace(UserId))
    {
      return RepositoryResponse.Failure<Cart>("User ID is invalid.");
    }

    var cartItems = this.dataContext.cart.Where(c => c.UserId == UserId).ToList();

    if (cartItems.Count == 0)
    {
      return RepositoryResponse.Failure<Cart>("No items to clear in the cart.");
    }

    this.dataContext.cart.RemoveRange(cartItems);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Cart>("Unable to clear cart at the moment.");
    }

    return RepositoryResponse.Success<Cart>("Cart cleared successfully");
  }

  public RepositoryResult<Cart> IncrementCartItem(string CartId, string UserId)
  {
    if (string.IsNullOrWhiteSpace(CartId))
    {
      return RepositoryResponse.Failure<Cart>("Cart ID is invalid.");
    }

    if (string.IsNullOrWhiteSpace(UserId))
    {
      return RepositoryResponse.Failure<Cart>("User ID is invalid.");
    }

    Cart? cart = this.dataContext.cart.FirstOrDefault(c => c.CartId == CartId && c.UserId == UserId);
    if (cart == null)
    {
      return RepositoryResponse.Failure<Cart>("Cart item not found.");
    }

    cart.Quantity++;

    this.dataContext.cart.Update(cart);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Cart>("Unable to increment cart item at the moment.");
    }

    return RepositoryResponse.Success<Cart>("Cart item incremented successfully", cart);
  }

  public RepositoryResult<Cart> DecrementCartItem(string CartId, string UserId)
  {
    if (string.IsNullOrWhiteSpace(CartId))
    {
      return RepositoryResponse.Failure<Cart>("Cart ID is invalid.");
    }

    if (string.IsNullOrWhiteSpace(UserId))
    {
      return RepositoryResponse.Failure<Cart>("User ID is invalid.");
    }

    Cart? cart = this.dataContext.cart.FirstOrDefault(c => c.CartId == CartId && c.UserId == UserId);
    if (cart == null)
    {
      return RepositoryResponse.Failure<Cart>("Cart item not found.");
    }

    if (cart.Quantity == 1)
    {
      return RepositoryResponse.Failure<Cart>("Cannot decrement, quantity is already at minimum.");
    }

    cart.Quantity--;

    this.dataContext.cart.Update(cart);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Cart>("Unable to decrement cart item at the moment.");
    }

    return RepositoryResponse.Success<Cart>("Cart item decremented successfully", cart);
  }
}