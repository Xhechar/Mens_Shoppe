
public interface IFavouriteRepository
{
    public RepositoryResult<Favourite> AddToFavourite(string UserId, string ProductId);
    public RepositoryResult<Favourite> RemoveFromFavourite(string UserId, string FavouriteId);
    public RepositoryResult<Favourite> GetFavouriteItems(string UserId);
    public RepositoryResult<Favourite> ClearFavourites(string UserId);
}