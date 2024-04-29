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
        public void CreateBookReviews(Review review)
        {
            _reviewRepository.Create(review);
        }

        public void DeleteBookReviews(int id)
        {
            _reviewRepository.Delete(id);
        }   

        public List<Review> GetAllReviews()
        {
            return _reviewRepository.GetAll();
        }

        public Review GetReviewById(int id)
        {
            return _reviewRepository.GetById(id);
        }

        public void UpdateBookReviews(Review review)
        {             
            _reviewRepository.Update(review);
        }

      
    }
}
