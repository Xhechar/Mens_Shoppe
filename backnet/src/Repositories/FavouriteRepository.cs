
using Microsoft.EntityFrameworkCore;

public class FavouriteRepository : IFavouriteRepository
{
    private readonly DataContext dataContext;

    public FavouriteRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public RepositoryResult<Favourite> AddToFavourite(string userId, string productId)
    {
        Favourite? existingFavourite = this.dataContext.favourite
            .FirstOrDefault(f => f.UserId == userId && f.ProductId == productId);

        if (existingFavourite != null)
        {
            return RepositoryResponse.Failure<Favourite>("This product is already in your favourites.");
        }

        var favourite = new Favourite
        {
          FavouriteId = Guid.NewGuid().ToString(),
          UserId = userId,
          ProductId = productId
        };

        this.dataContext.favourite.Add(favourite);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Favourite>("Unable to add favourite at the moment.");
        }

        return RepositoryResponse.Success<Favourite>("Product added to favourites successfully", favourite);
    }

    public RepositoryResult<Favourite> RemoveFromFavourite(string userId, string favouriteId)
    {
        Favourite? favourite = this.dataContext.favourite
            .FirstOrDefault(f => f.UserId == userId && f.FavouriteId == favouriteId);

        if (favourite == null)
        {
            return RepositoryResponse.Failure<Favourite>("Favourite item not found.");
        }

        this.dataContext.favourite.Remove(favourite);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Favourite>("Unable to remove favourite at the moment.");
        }

        return RepositoryResponse.Success<Favourite>("Favourite item removed successfully", favourite);
    }

    public RepositoryResult<Favourite> GetFavouriteItems(string userId)
    {
        var favourites = this.dataContext.favourite
            .Include(f => f.FavouriteProduct)
            .Include(f => f.Owner)
            .Where(f => f.UserId == userId)
            .ToList();

        if (favourites.Count == 0)
        {
            return RepositoryResponse.Failure<Favourite>("No favourite items found.");
        }

        return RepositoryResponse.Success<Favourite>("Favourite items retrieved successfully", dataList: favourites);
    }

    public RepositoryResult<Favourite> ClearFavourites(string userId)
    {
        var favourites = this.dataContext.favourite
            .Where(f => f.UserId == userId)
            .ToList();

        if (favourites.Count == 0)
        {
            return RepositoryResponse.Failure<Favourite>("No favourite items to clear.");
        }

        this.dataContext.favourite.RemoveRange(favourites);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Favourite>("Unable to clear favourites at the moment.");
        }

        return RepositoryResponse.Success<Favourite>("All favourite items cleared successfully");
    }
}