using Book_Management_API.Database;

namespace Book_Management_API.Service
{
    public class RecommendationService
    {
        private readonly BookContext _context;

        public RecommendationService(BookContext context)
        {
            _context = context;
        }

    }
}
