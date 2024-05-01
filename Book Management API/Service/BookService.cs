using Book_Management_API.Data;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Book_Management_API.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public void AddBook(BookDto book)
        {
            if (book is null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            var existingBook = _bookRepository.GetByTitle(book.Title);
            if (existingBook != null)
            {
                throw new ConflictErrorException("Book already exists");
            }
            var newBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Genre = book.Genre,
                Reviews = new List<Review>()
            };
            _bookRepository.Create(newBook);
        }

        public void DeleteBook(int id)
        {
            var book = _bookRepository.Get(id);
            if (book is null)
            {
                throw new NotFoundErrorException("Book not found");
            }
            _bookRepository.Delete(id);
        }

        public void EditBook(int id, BookDto book)
        {
            var existingBook = _bookRepository.Get(id);
            if (existingBook is null)
            {
                throw new NotFoundErrorException("Book not found");
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;
            existingBook.Genre = book.Genre;
            _bookRepository.Update(existingBook);
        }

        public List<Book> FilterBooks(string title = "", int rating = 0, int publishYear = 0, string genre = "", int limit = 0)
        {
            IQueryable<Book> books = _bookRepository.GetAll().AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                books = books.Where(b => b.Title.ToLower().Contains(title.ToLower()));
            }
            if (rating > 0)
            {
                books = books.Include(x => x.Reviews).Where(b => b.Reviews.Any(r => r.Rating == rating));
            }
            if (publishYear > 0)
            {
                books = books.Where(b => b.Year == publishYear);
            }
            if (!string.IsNullOrEmpty(genre))
            {
                books = books.Where(b => b.Genre.ToLower().Contains(genre.ToLower()));
            }
            return limit > 0 ? books.Take(limit).ToList() : books.ToList();
        }

        public Book GetBook(int id)
        {
            var book = _bookRepository.Get(id);
            if (book is null)
            {
                throw new NotFoundErrorException("Book not found");
            }
            return book;
        }

        public List<Book> SortBooksByNumberOfReviews()
        {
            return _bookRepository.GetAll().OrderByDescending(b => b.Reviews.Count).ToList();
        }
    }
}
