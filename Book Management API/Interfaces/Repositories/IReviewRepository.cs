using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        void Create(Review review);
        void Update(Review review);
        void Delete(int id);
    }
}
