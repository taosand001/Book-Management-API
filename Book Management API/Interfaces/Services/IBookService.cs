using Book_Management_API.Dto;
using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Services
{
    public interface IBookService
    {
        void AddBook(BookDto book);
        void EditBook(int id, BookDto book);
        void DeleteBook(int id);
        Book GetBook(int id);
        List<Book> FilterBooks(string title = "", int rating = 0, int publishYear = 0, string genre = "", int limit = 0);
        List<Book> SortBooksByNumberOfReviews();
    }
}
