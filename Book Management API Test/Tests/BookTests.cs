using AutoFixture;
using Book_Management_API.Data;
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
        private Fixture _fixture = new Fixture();

        public BookTests()
        {
            _fixture.Customizations.Add(new BookSpecimenBuilder());
        }

        [Theory, BookData]
        public void Return_Book_Created(BookDto book)
        {
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(x => x.GetByTitle(book.Title)).Returns((Book)null);
            var bookService = new BookService(bookRepository.Object);


            bookService.AddBook(book);

            bookRepository.Verify(x => x.Create(It.IsAny<Book>()), Times.Once);
        }

        [Theory, BookData]
        public void Return_Book_Deleted(int id)
        {
            var book = _fixture.Create<Book>();
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(x => x.Get(id)).Returns(book);
            var bookService = new BookService(bookRepository.Object);

            bookService.DeleteBook(id);

            bookRepository.Verify(x => x.Delete(id), Times.Once);
        }

        [Theory, BookData]
        public void Return_Book_Edited(int id, BookDto bookDto)
        {
            var book = _fixture.Create<Book>();
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(x => x.Get(id)).Returns(book);
            var bookService = new BookService(bookRepository.Object);

            bookService.EditBook(id, bookDto);

            bookRepository.Verify(x => x.Update(It.IsAny<Book>()), Times.Once);
        }

        [Theory, BookData]
        public void Return_Book_Not_Found_When_Deleting(int id)
        {
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(x => x.Get(id)).Returns((Book)null);
            var bookService = new BookService(bookRepository.Object);

            Assert.Throws<NotFoundErrorException>(() => bookService.DeleteBook(id));

            bookRepository.Verify(x => x.Delete(id), Times.Never);
        }

        [Theory, BookData]
        public void Return_Book_Not_Found_When_Editing(int id, BookDto bookDto)
        {
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(x => x.Get(id)).Returns((Book)null);
            var bookService = new BookService(bookRepository.Object);

            Assert.Throws<NotFoundErrorException>(() => bookService.EditBook(id, bookDto));

            bookRepository.Verify(x => x.Update(It.IsAny<Book>()), Times.Never);
        }

        [Theory, BookData]
        public void Return_Book_Conflict_When_Adding(BookDto book)
        {
            var existingBook = _fixture.Create<Book>();
            var bookRepository = new Mock<IBookRepository>();
            bookRepository.Setup(x => x.GetByTitle(book.Title)).Returns(existingBook);
            var bookService = new BookService(bookRepository.Object);

            Assert.Throws<ConflictErrorException>(() => bookService.AddBook(book));

            bookRepository.Verify(x => x.Create(It.IsAny<Book>()), Times.Never);
        }
    }
}