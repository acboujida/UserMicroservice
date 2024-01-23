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
    }
}
