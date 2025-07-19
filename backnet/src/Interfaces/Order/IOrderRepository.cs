
public interface IOrderRepository
{
    public RepositoryResult<Order> CreateOrder(string UserId);
    public RepositoryResult<Order> GetOrderById(string OrderId);
    public RepositoryResult<Order> GetOrdersByUserId(string UserId);
    public RepositoryResult<Order> GetAllOrders();
    public RepositoryResult<Order> GetCompletedOrders();
    public RepositoryResult<Order> GetPendingOrders();
    public RepositoryResult<Order> GetOrdersByProductId(string ProductId);
    public RepositoryResult<Order> ToggleOrderStatus(string OrderId);
    public RepositoryResult<Order> DeleteOrder(string OrderId);
}