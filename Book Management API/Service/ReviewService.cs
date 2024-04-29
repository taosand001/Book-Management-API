using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;

namespace Book_Management_API.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public void CreateBookReviews(int bookId)
        {
            throw new NotImplementedException();
        }

        public void DeleteBookReviews(int id)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public void UpdateBookReviews(int id)
        {
            throw new NotImplementedException();
        }
    }
}
