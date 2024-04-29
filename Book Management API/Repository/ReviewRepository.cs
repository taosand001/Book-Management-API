using Book_Management_API.Database;
using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Model;

namespace Book_Management_API.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BookContext _context;

        public ReviewRepository(BookContext context)
        {
            _context = context;
        }

        public void Create(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var review = GetById(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }

        public void Update(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }

        public Review GetById(int id)
        {
            return _context.Reviews.FirstOrDefault(r => r.Id == id);
        }

        public List<Review> GetAll()
        {
            return _context.Reviews.ToList();
        }
    }
}
