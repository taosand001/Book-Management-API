using Book_Management_API.Dto;
using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Services
{
    public interface IReviewService
    {
        public void CreateBookReviews(ReviewDto review, string UserId);
        public void UpdateBookReviews(ReviewDto review, int id, string UserId,string role);
        void DeleteBookReviews(int id);
        List<Review> GetAllReviews(int BookId);
        public Review GetReviewById(int id);


    }
}
