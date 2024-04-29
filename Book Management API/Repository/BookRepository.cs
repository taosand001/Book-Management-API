using Book_Management_API.Database;
using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }
        public void Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            _context.Books.Remove(Get(Id));
            _context.SaveChanges();
        }

        public Book Get(int Id)
        {
            return _context.Books.Include(r => r.Reviews).FirstOrDefault(b => b.Id == Id);
        }

        public Book GetByTitle(string title)
        {
            return _context.Books.Include(r => r.Reviews).FirstOrDefault(b => b.Title == title);
        }

        public List<Book> GetAll()
        {
            return _context.Books.Include(r => r.Reviews).ToList();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
    }
}
