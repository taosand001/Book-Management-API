using Book_Management_API.Data;
using Book_Management_API.Dto;
using Book_Management_API.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]
        public IActionResult AddBook([FromBody] BookDto book)
        {
            try
            {
                _bookService.Addbook(book);
                return Created("", new { message = "Book has been added" });
            }
            catch (ConflictErrorException Ex)
            {
                return BadRequest(Ex.Message);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetBook(int id)
        {
            try
            {
                var book = _bookService.GetBook(id);
                return Ok(book);
            }
            catch (NotFoundErrorException Ex)
            {
                return NotFound(Ex.Message);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult EditBook(int id, [FromBody] BookDto book)
        {
            try
            {
                _bookService.EditBook(id, book);
                return Ok();
            }
            catch (NotFoundErrorException Ex)
            {
                return NotFound(Ex.Message);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.DeleteBook(id);
                return NoContent();
            }
            catch (NotFoundErrorException Ex)
            {
                return NotFound(Ex.Message);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpGet("filterBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult FilterBooks([FromQuery] string title, [FromQuery] int rating = 0, [FromQuery] int publishYear = 0, [FromQuery] int limit = 0)
        {
            try
            {
                var books = _bookService.FilterBooks(title, rating, publishYear, limit);
                return Ok(books);
            }
            catch (NotFoundErrorException Ex)
            {
                return NotFound(Ex.Message);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
