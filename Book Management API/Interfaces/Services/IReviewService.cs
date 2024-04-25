using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Services
{
    public interface IReviewService
    {
        void CreateBookReviews(int bookId);
        void UpdateBookReviews(int id);
        void DeleteBookReviews(int id);
        List<Review> GetAllReviews();
    }
}
