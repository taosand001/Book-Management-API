using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Repositories;
using Book_Management_API.Model;
using Book_Management_API.Service;
using Book_Management_API_Test.Data;
using Moq;

namespace Book_Management_API_Test.Tests
{
    public class BookTests
    {
        [Theory, BookData]
        public void Return_Book_Created(BookDto book)
        {
            var newBook = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Genre = book.Genre,
                Reviews = new List<Review>()
            };
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(x => x.GetByTitle(book.Title)).Returns((Book)null);
            var bookService = new BookService(bookRepository.Object);


            bookService.Addbook(book);

            bookRepository.Verify(x => x.Create(It.IsAny<Book>()), Times.Once);
        }
    }
}