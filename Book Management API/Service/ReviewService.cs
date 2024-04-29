using Book_Management_API.Data;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;
using Book_Management_API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_API.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public void CreateBookReviews(ReviewDto review , string userId)
        {
            var existingReview = _reviewRepository.GetAll(review.BookId).FirstOrDefault(r => r.BookId == review.BookId && r.UserId == userId);

            if (existingReview != null)
            {
                throw new ConflictErrorException("Review already exists");
                
            }
            else
            {
                Review newReview = new Review
                {
                    BookId = review.BookId,
                    Comment = review.Comment,
                    Rating = review.Rating,
                    UserId = userId

                };
                _reviewRepository.Create(newReview);
            }
            
        }

        public void DeleteBookReviews(int id)
        {
            Review review = _reviewRepository.GetById(id);
            if (review is null)
            {
                throw new NotFoundErrorException("Review not found");
            }
            _reviewRepository.Delete(id);
        }   

        public List<Review> GetAllReviews(int bookId)
        {
            return _reviewRepository.GetAll(bookId);
        }

        public Review GetReviewById(int id)
        {
            var review =  _reviewRepository.GetById(id);
            if (review is null)
            {
                throw new NotFoundErrorException("Review not found");
            }
            return review;
        }

        public void UpdateBookReviews(ReviewDto review,int id,string userId, string role    )
        {
            Review existibngReview = _reviewRepository.GetById(id);
            if (existibngReview is null )
            {
                throw new NotFoundErrorException("Review does not exist");
            }
            if(existibngReview.UserId != userId && role != "Admin" )
            {
                throw new NotFoundErrorException("Cannot update someone else review");
            }           
            existibngReview.Rating = review.Rating;
            existibngReview.Comment = review.Comment;
            _reviewRepository.Update(existibngReview);
        }

      
    }
}
