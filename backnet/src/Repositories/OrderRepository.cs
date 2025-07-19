
using Microsoft.EntityFrameworkCore;

public class OrderRepository : IOrderRepository
{
  private readonly DataContext dataContext;

  public OrderRepository(DataContext dataContext)
  {
    this.dataContext = dataContext;
  }

  public RepositoryResult<Order> CreateOrder(string UserId)
  {
    if (string.IsNullOrWhiteSpace(UserId))
    {
      return RepositoryResponse.Failure<Order>("User ID is invalid.");
    }

    User? user = this.dataContext.user.FirstOrDefault(u => u.UserId == UserId);
    if (user == null)
    {
      return RepositoryResponse.Failure<Order>("User not found.");
    }

    var cartItems = this.dataContext.cart.Include(c => c.CartProduct)
      .Where(c => c.UserId == UserId).ToList();

    if (cartItems.Count == 0)
    {
      return RepositoryResponse.Failure<Order>("No items in the cart to create an order.");
    }

    var orders = cartItems.Select(cartItem => new Order
    {
      OrderId = Guid.NewGuid().ToString(),
      UserId = UserId,
      ProductId = cartItem.ProductId,
      Quantity = cartItem.Quantity,
      TotalPrice = cartItem.CartProduct.Price * cartItem.Quantity,
      CreatedAt = DateTime.UtcNow
    }).ToList();

    this.dataContext.order.AddRange(orders);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Order>("Unable to create order at the moment.");
    }

    this.dataContext.cart.RemoveRange(cartItems);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Order>("Unable to clear cart after order creation.");
    }

    return RepositoryResponse.Success<Order>("Order created successfully", dataList: orders);
  }

  public RepositoryResult<Order> GetOrderById(string OrderId)
  {
    if (string.IsNullOrWhiteSpace(OrderId))
    {
      return RepositoryResponse.Failure<Order>("Order ID is invalid.");
    }

    Order? order = this.dataContext.order.Include(o => o.Owner).Include(o => o.PurchasedProduct).FirstOrDefault(o => o.OrderId == OrderId);

    if (order == null)
    {
      return RepositoryResponse.Failure<Order>("Order not found.");
    }

    return RepositoryResponse.Success<Order>("Order retrieved successfully", order);
  }

  public RepositoryResult<Order> GetOrdersByUserId(string UserId)
  {
    if (string.IsNullOrWhiteSpace(UserId))
    {
      return RepositoryResponse.Failure<Order>("User ID is invalid.");
    }

    var orders = this.dataContext.order.Include(o => o.Owner).Include(o => o.PurchasedProduct)
      .Where(o => o.UserId == UserId).OrderBy(o => o.CreatedAt).ToList();

    if (orders.Count == 0)
    {
      return RepositoryResponse.Failure<Order>("No orders found for this user.");
    }

    return RepositoryResponse.Success<Order>("Orders retrieved successfully", dataList: orders);
  }

  public RepositoryResult<Order> GetAllOrders()
  {
    var orders = this.dataContext.order.Include(o => o.Owner).Include(o => o.PurchasedProduct)
      .OrderBy(o => o.CreatedAt).ToList();

    if (orders.Count == 0)
    {
      return RepositoryResponse.Failure<Order>("No orders found.");
    }

    return RepositoryResponse.Success<Order>("All orders retrieved successfully", dataList: orders);
  }

  public RepositoryResult<Order> GetCompletedOrders()
  {
    var completedOrders = this.dataContext.order.Include(o => o.Owner).Include(o => o.PurchasedProduct)
      .Where(o => o.IsOrderCompleted).OrderBy(o => o.CreatedAt).ToList();

    if (completedOrders.Count == 0)
    {
      return RepositoryResponse.Failure<Order>("No completed orders found.");
    }

    return RepositoryResponse.Success<Order>("Completed orders retrieved successfully", dataList: completedOrders);
  }

  public RepositoryResult<Order> GetPendingOrders()
  {
    var pendingOrders = this.dataContext.order.Include(o => o.Owner).Include(o => o.PurchasedProduct)
      .Where(o => !o.IsOrderCompleted).OrderBy(o => o.CreatedAt).ToList();

    if (pendingOrders.Count == 0)
    {
      return RepositoryResponse.Failure<Order>("No pending orders found.");
    }

    return RepositoryResponse.Success<Order>("Pending orders retrieved successfully", dataList: pendingOrders);
  }

  public RepositoryResult<Order> GetOrdersByProductId(string ProductId)
  {
    if (string.IsNullOrWhiteSpace(ProductId))
    {
      return RepositoryResponse.Failure<Order>("Product ID is invalid.");
    }

    Product? product = this.dataContext.product.FirstOrDefault(p => p.ProductId == ProductId);

    if (product == null)
    {
      return RepositoryResponse.Failure<Order>("Product not found.");
    }

    var orders = this.dataContext.order.Include(o => o.Owner).Include(o => o.PurchasedProduct)
      .Where(o => o.ProductId == ProductId).OrderBy(o => o.CreatedAt).ToList();

    if (orders.Count == 0)
    {
      return RepositoryResponse.Failure<Order>("No orders found for this product.");
    }

    return RepositoryResponse.Success<Order>("Orders for product retrieved successfully", dataList: orders);
  }

  public RepositoryResult<Order> ToggleOrderStatus(string OrderId)
  {
    if (string.IsNullOrWhiteSpace(OrderId))
    {
      return RepositoryResponse.Failure<Order>("Order ID is invalid.");
    }

    Order? order = this.dataContext.order.FirstOrDefault(o => o.OrderId == OrderId);

    if (order == null)
    {
      return RepositoryResponse.Failure<Order>("Order not found.");
    }

    order.IsOrderCompleted = !order.IsOrderCompleted;

    this.dataContext.order.Update(order);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Order>("Unable to update order status at the moment.");
    }

    return RepositoryResponse.Success<Order>("Order status updated successfully", order);
  }

  public RepositoryResult<Order> DeleteOrder(string OrderId)
  {
    if (string.IsNullOrWhiteSpace(OrderId))
    {
      return RepositoryResponse.Failure<Order>("Order ID is invalid.");
    }

    Order? order = this.dataContext.order.FirstOrDefault(o => o.OrderId == OrderId);

    if (order == null)
    {
      return RepositoryResponse.Failure<Order>("Order not found.");
    }

    this.dataContext.order.Remove(order);

    if (!Save.SaveChanges(this.dataContext))
    {
      return RepositoryResponse.Failure<Order>("Unable to delete order at the moment.");
    }

    return RepositoryResponse.Success<Order>("Order deleted successfully", order);
  }
}