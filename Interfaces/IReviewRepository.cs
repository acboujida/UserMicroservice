using UserMicroservice.Models;

namespace UserMicroservice.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        bool ReviewExists(int id);
        User GetUserOfReview(int id);
        string GetBookOfReview(int id);
        bool IsUserReviewOwner(int id, string userid);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool Save();
    }
}
