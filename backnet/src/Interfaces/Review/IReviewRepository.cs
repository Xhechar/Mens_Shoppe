
public interface IReviewRepository
{
    public RepositoryResult<Review> CreateReview(CreateReviewDto createReviewDto, string UserId, string ProductId);
    public RepositoryResult<Review> UpdateReview(string UserId, string ReviewId, UpdateReviewDto updateReviewDto);
    public RepositoryResult<Review> DeleteReview(string ReviewId, string UserId);
    public RepositoryResult<Review> GetReviewById(string ReviewId);
    public RepositoryResult<Review> GetAllReviews();
    public RepositoryResult<Review> GetReviewsByProductId(string productId);
    public RepositoryResult<Review> GetReviewsByUserId(string UserId);
}