
using Microsoft.EntityFrameworkCore;

public class ReviewRepository : IReviewRepository
{
    private readonly DataContext dataContext;

    public ReviewRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public RepositoryResult<Review> CreateReview(CreateReviewDto createReviewDto, string UserId, string ProductId)
    {
        if (string.IsNullOrWhiteSpace(UserId))
        {
            return RepositoryResponse.Failure<Review>("User ID is invalid.");
        }

        if (string.IsNullOrWhiteSpace(ProductId))
        {
            return RepositoryResponse.Failure<Review>("Product ID is invalid.");
        }

        User? user = this.dataContext.user.FirstOrDefault(u => u.UserId == UserId);
        if (user == null)
        {
            return RepositoryResponse.Failure<Review>("User not found.");
        }

        Product? product = this.dataContext.product.FirstOrDefault(p => p.ProductId == ProductId);
        if (product == null)
        {
            return RepositoryResponse.Failure<Review>("Product not found.");
        }

        Review? existingReview = this.dataContext.review.FirstOrDefault(r => r.UserId == UserId && r.ProductId == ProductId);
        if (existingReview != null)
        {
            return RepositoryResponse.Failure<Review>("You have already reviewed this product.");
        }

        Order? orderExists = this.dataContext.order.FirstOrDefault(o => o.UserId == UserId && o.ProductId == ProductId);
        if (orderExists == null)
        {
            return RepositoryResponse.Failure<Review>("You must purchase the product before reviewing it.");
        }

        Review review = new Review
        {
            ReviewId = Guid.NewGuid().ToString(),
            ProductId = ProductId,
            UserId = UserId,
            Rating = createReviewDto.Rating,
            Comment = createReviewDto.Comment
        };

        this.dataContext.review.Add(review);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Review>("Unable to create review at the moment.");
        }

        return RepositoryResponse.Success<Review>("Review created successfully", review);
    }

    public RepositoryResult<Review> UpdateReview(string UserId, string ReviewId, UpdateReviewDto updateReviewDto)
    {
        if (string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(ReviewId))
        {
            return RepositoryResponse.Failure<Review>("User ID or Review ID is invalid.");
        }

        Review? review = this.dataContext.review.FirstOrDefault(r => r.ReviewId == ReviewId && r.UserId == UserId);
        if (review == null)
        {
            return RepositoryResponse.Failure<Review>("Review not found.");
        }

        review.Rating = updateReviewDto.Rating;
        review.Comment = updateReviewDto.Comment;
        review.UpdatedAt = DateTime.Now;

        this.dataContext.review.Update(review);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Review>("Unable to update review at the moment.");
        }

        return RepositoryResponse.Success<Review>("Review updated successfully", review);
    }

    public RepositoryResult<Review> DeleteReview(string ReviewId, string UserId)
    {
        if (string.IsNullOrWhiteSpace(ReviewId) || string.IsNullOrWhiteSpace(UserId))
        {
            return RepositoryResponse.Failure<Review>("Review ID or User ID is invalid.");
        }

        Review? review = this.dataContext.review.FirstOrDefault(r => r.ReviewId == ReviewId && r.UserId == UserId);
        if (review == null)
        {
            return RepositoryResponse.Failure<Review>("Review not found.");
        }

        this.dataContext.review.Remove(review);

        if (!Save.SaveChanges(this.dataContext))
        {
            return RepositoryResponse.Failure<Review>("Unable to delete review at the moment.");
        }

        return RepositoryResponse.Success<Review>("Review deleted successfully", review);
    }

    public RepositoryResult<Review> GetReviewById(string ReviewId)
    {
        if (string.IsNullOrWhiteSpace(ReviewId))
        {
            return RepositoryResponse.Failure<Review>("Review ID is invalid.");
        }

        Review? review = this.dataContext.review
        .Include(r => r.Owner)
        .Include(r => r.PurchasedProduct)
        .FirstOrDefault(r => r.ReviewId == ReviewId);
        if (review == null)
        {
            return RepositoryResponse.Failure<Review>("Review not found.");
        }

        return RepositoryResponse.Success<Review>("Review successfully retrieved.", review);
    }

    public RepositoryResult<Review> GetAllReviews()
    {
        var reviews = this.dataContext.review
        .Include(r => r.Owner)
        .Include(r => r.PurchasedProduct)
        .ToList();

        if (reviews.Count == 0)
        {
            return RepositoryResponse.Failure<Review>("No reviews found.");
        }

        return RepositoryResponse.Success<Review>("All reviews retrieved successfully.", dataList: reviews);
    }

    public RepositoryResult<Review> GetReviewsByProductId(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            return RepositoryResponse.Failure<Review>("Product ID is invalid.");
        }

        var reviews = this.dataContext.review
            .Include(r => r.Owner)
            .Include(r => r.PurchasedProduct)
        .Where(r => r.ProductId == productId).ToList();
        if (reviews.Count == 0)
        {
            return RepositoryResponse.Failure<Review>("No reviews found for this product.");
        }

        return RepositoryResponse.Success<Review>("Reviews for product retrieved successfully.", dataList: reviews);
    }

    public RepositoryResult<Review> GetReviewsByUserId(string UserId)
    {
        if (string.IsNullOrWhiteSpace(UserId))
        {
            return RepositoryResponse.Failure<Review>("User ID is invalid.");
        }

        var reviews = this.dataContext.review
          .Include(r => r.Owner)
          .Include(r => r.PurchasedProduct)
          .Where(r => r.UserId == UserId).ToList();
          
        if (reviews.Count == 0)
        {
            return RepositoryResponse.Failure<Review>("No reviews found for this user.");
        }

        return RepositoryResponse.Success<Review>("Reviews by user retrieved successfully.", dataList: reviews);
    }
}