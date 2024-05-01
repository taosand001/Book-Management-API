using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Services
{
    public interface IRecommendationService
    {
        List<Book> GetRecommendation(string UserId);
    }
}