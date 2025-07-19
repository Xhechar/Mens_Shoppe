
public interface ICartRepository
{
    public RepositoryResult<Cart> AddToCart(string UserId, string ProductId, CreateCartDto createCartDto);
    public RepositoryResult<Cart> RemoveFromCart(string UserId, string CartId);
    public RepositoryResult<Cart> GetCartItems(string UserId);
    public RepositoryResult<Cart> ClearCart(string UserId);
    public RepositoryResult<Cart> IncrementCartItem(string CartId, string UserId);
    public RepositoryResult<Cart> DecrementCartItem(string CartId, string UserId);
}