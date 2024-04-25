using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Repositories
{
    public interface IBookRepository
    {
        void Create(Book book);
        void Update(Book book);
        List<Book> GetAll();
        void Get(int Id);
        void Delete(int Id);
    }
}
