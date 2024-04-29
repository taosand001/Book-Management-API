using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Services
{
    public interface IReviewService
    {
        void CreateBookReviews(Review review);
        void UpdateBookReviews(Review review);
        void DeleteBookReviews(int id);
        List<Review> GetAllReviews();
        
    }
}
