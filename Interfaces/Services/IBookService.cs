using Book_Management_API.Model;

namespace Book_Management_API.Interfaces.Services
{
    public interface IBookService
    {
        void Addbook(Book book);
        void EditBook(Book book);
        void DeleteBook(Book book);
        List<Book> SearchBooks(string bookTitle);
        List<Book> FilterByRating(int rating);
        List<Book> FilterByPublication(int year);
        List<Book> SortBooksByNumberOfReviews();
    }
}
