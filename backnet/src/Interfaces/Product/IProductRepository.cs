
public interface IProductRepository
{
    public RepositoryResult<Product> CreateProduct(CreateProductDto createProductDto);
    public RepositoryResult<Product> UpdateProduct(string productId, UpdateProductDto updateProductDto);
    public RepositoryResult<Product> DeleteProduct(string productId);
    public RepositoryResult<Product> GetProductById(string productId);
    public RepositoryResult<Product> GetAllProducts();
    public RepositoryResult<Product> GetProductsByType(string type);
    public RepositoryResult<Product> SearchProducts(string searchTerm);
}