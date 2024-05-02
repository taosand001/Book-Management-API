using Book_Management_API.Database;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;

namespace Book_Management_API.Service
{
    public class RecommendationService : IRecommendationService
    {
        private readonly BookContext _context;

        public RecommendationService(BookContext context)
        {
            _context = context;
        }


        public List<Book> GetRecommendation(string UserId)
        {

            var allReviewsExceptCurrentUser = _context.Reviews
                .Where(r => r.UserId != UserId)
                .ToList();

            var similarityScores = allReviewsExceptCurrentUser
                .GroupBy(r => r.UserId)
                .Select(r => new
                {
                    UserId = r.Key,
                    Similarity = CalculateSimilarity(UserId, r.Key)
                })
                .OrderByDescending(s => s.Similarity)
                .Take(5)
                .ToList();

            var similarUserIds = similarityScores.Select(s => s.UserId).ToList();

            var reviewsOfSimilarUsers = allReviewsExceptCurrentUser
                .Where(r => similarUserIds.Contains(r.UserId))
                .ToList();

            var recommendedBookIds = reviewsOfSimilarUsers
                .Where(r => r.Rating >= 3)
                .Select(r => r.BookId)
                .Distinct()
                .ToList();

            var recommendedBooks = _context.Books
                .Where(b => recommendedBookIds.Contains(b.Id) && !_context.Reviews.Any(r => r.UserId == UserId && r.BookId == b.Id))
                .ToList();

            return recommendedBooks;
        }

        public double CalculateSimilarity(string UserId, string UserId2)
        {
            var user1 = _context.Reviews.Where(r => r.UserId == UserId).ToDictionary(r => r.BookId, r => r.Rating);
            var user2 = _context.Reviews.Where(r => r.UserId == UserId2).ToDictionary(r => r.BookId, r => r.Rating);

            var commonBooks = user1.Keys.Intersect(user2.Keys);
            if (commonBooks.Any())
            {
                var sum = commonBooks.Sum(b => Math.Pow(user1[b] - user2[b], 2));
                return 1 / (1 + Math.Sqrt(sum));
            }
            return 0;
        }
    }
}
