using Book_Management_API.Interfaces.Services;
using Book_Management_API.Model;

namespace Book_Management_API.Service
{
    public class BookService : IBookService
    {
        public void Addbook(Book book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void EditBook(Book book)
        {
            throw new NotImplementedException();
        }

        public List<Book> FilterByPublication(int year)
        {
            throw new NotImplementedException();
        }

        public List<Book> FilterByRating(int rating)
        {
            throw new NotImplementedException();
        }

        public List<Book> SearchBooks(string bookTitle)
        {
            throw new NotImplementedException();
        }

        public List<Book> SortBooksByNumberOfReviews()
        {
            throw new NotImplementedException();
        }
    }
}
