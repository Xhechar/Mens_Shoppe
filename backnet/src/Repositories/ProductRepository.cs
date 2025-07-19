using Microsoft.EntityFrameworkCore;
public class ProductRepository : IProductRepository
{
    private readonly DataContext dataContext;

    public ProductRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public RepositoryResult<Product> CreateProduct(CreateProductDto productDto)
    {
        Product product = new Product
        {
            ProductId = Guid.NewGuid().ToString(),
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Type = productDto.Type,
            Images = productDto.Images,
            Size = productDto.Size,
            Quantity = productDto.Quantity,
            StockLimit = productDto.StockLimit
        };

        this.dataContext.product.Add(product);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Product>("Unable to create product at the moment.");
        }

        return RepositoryResponse.Success<Product>("Product created successfully", product);
    }

    public RepositoryResult<Product> UpdateProduct(string productId, UpdateProductDto updateProductDto)
    {
        if (string.IsNullOrWhiteSpace(productId)) return RepositoryResponse.Failure<Product>("Product Id is invalid.");

        Product? product = this.dataContext.product.FirstOrDefault(p => p.ProductId == productId);

        if (product == null) return RepositoryResponse.Failure<Product>("Product not found.");

        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.Type = updateProductDto.Type;
        product.Images = updateProductDto.Images;
        product.Size = updateProductDto.Size;
        product.Quantity = updateProductDto.Quantity;
        product.StockLimit = updateProductDto.StockLimit;

        this.dataContext.product.Update(product);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Product>("Unable to update product at the moment.");
        }

        return RepositoryResponse.Success<Product>("Product updated successfully", product);
    }

    public RepositoryResult<Product> DeleteProduct(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId)) return RepositoryResponse.Failure<Product>("Product Id is invalid.");

        Product? product = this.dataContext.product.FirstOrDefault(p => p.ProductId == productId);

        if (product == null) return RepositoryResponse.Failure<Product>("Product not found.");

        this.dataContext.product.Remove(product);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Product>("Unable to delete product at the moment.");
        }

        return RepositoryResponse.Success<Product>("Product deleted successfully");
    }

    public RepositoryResult<Product> GetProductById(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId)) return RepositoryResponse.Failure<Product>("Product Id is invalid.");

        Product? product = this.dataContext.product
            .Include(p => p.ProductCarts)
            .Include(p => p.ProductFavourites)
            .Include(p => p.ProductReviews)
            .Include(p => p.ProductOrders)
        .FirstOrDefault(p => p.ProductId == productId);

        if (product == null) return RepositoryResponse.Failure<Product>("Product not found.");

        return RepositoryResponse.Success<Product>("Product retrieved successfully", product);
    }

    public RepositoryResult<Product> GetAllProducts()
    {
        var products = this.dataContext.product
            .Include(p => p.ProductCarts)
            .Include(p => p.ProductFavourites)
            .Include(p => p.ProductReviews)
            .Include(p => p.ProductOrders)
        .ToList();

        if (products.Count == 0) return RepositoryResponse.Failure<Product>("No products found.");

        return RepositoryResponse.Success<Product>("Products retrieved successfully", dataList: products);
    }

    public RepositoryResult<Product> GetProductsByType(string type)
    {
        if (string.IsNullOrWhiteSpace(type)) return RepositoryResponse.Failure<Product>("Product type is invalid.");

        var products = this.dataContext.product
            .Include(p => p.ProductCarts)
            .Include(p => p.ProductFavourites)
            .Include(p => p.ProductReviews)
            .Include(p => p.ProductOrders)
        .Where(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();

        if (products.Count == 0) return RepositoryResponse.Failure<Product>("No products found for the specified type.");

        return RepositoryResponse.Success<Product>("Products by type retrieved successfully", dataList: products);
    }

    public RepositoryResult<Product> SearchProducts(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return RepositoryResponse.Failure<Product>("Search term is invalid.");

        var products = this.dataContext.product
            .Include(p => p.ProductCarts)
            .Include(p => p.ProductFavourites)
            .Include(p => p.ProductReviews)
            .Include(p => p.ProductOrders)
            .Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        p.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (products.Count == 0) return RepositoryResponse.Failure<Product>("No products found matching the search term.");

        return RepositoryResponse.Success<Product>("Products found for the search term", dataList: products);
    }
}