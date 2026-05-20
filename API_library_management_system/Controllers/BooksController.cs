using Library.Application.DTOs.Features.Books.Commands;
using Library.Application.DTOs.Features.Books.DTos;
using Library.Application.DTOs.Features.Books.Queries;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API_library_management_system.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks(
            int pageNumber = 1,
            int pageSize = 5)
        {
            var query = new GetAllBooksQuery();

            var books = await _bookRepository.GetAllAsync();

            var pagedBooks = books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var response = pagedBooks.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                YearOfPublication = book.YearofPublication,
                ISBN = book.ISBN,
                Price = book.Price,
                CategoryId = book.CategoryId,
                CategoryName = book.Category.Name
            });

            return Ok(response);
        }

        // GET: api/books/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var query = new GetBookByIdQuery(id);

            var book = await _bookRepository.GetByIdAsync(query.Id);

            if (book == null)
            {
                return NotFound();
            }

            var response = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                YearOfPublication = book.YearofPublication,
                ISBN = book.ISBN,
                Price = book.Price,
                CategoryId = book.CategoryId,
                CategoryName = book.Category.Name
            };

            return Ok(response);
        }

        // GET: api/books/search?term=harry
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(string term)
        {
            var query = new SearchBooksQuery(term);

            var books = await _bookRepository.SearchAsync(query.Term);

            var response = books.Select(book => new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                YearOfPublication = book.YearofPublication,
                ISBN = book.ISBN,
                Price = book.Price,
                CategoryId = book.CategoryId,
                CategoryName = book.Category?.Name
            });

            return Ok(response);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookCommand command)
        {
            var book = new Book
            {
                Title = command.Title,
                YearofPublication = command.YearOfPublication,
                ISBN = command.ISBN,
                Price = command.Price,
                CategoryId = command.CategoryId
            };

            await _bookRepository.AddAsync(book);

            var response = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                YearOfPublication = book.YearofPublication,
                ISBN = book.ISBN,
                Price = book.Price,
                CategoryId = book.CategoryId
            };

            return Ok(response);
        }

        // PUT: api/books/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookCommand command)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = command.Title;
            existingBook.YearofPublication = command.YearOfPublication;
            existingBook.ISBN = command.ISBN;
            existingBook.Price = command.Price;
            existingBook.CategoryId = command.CategoryId;

            await _bookRepository.UpdateAsync(existingBook);

            var response = new BookDto
            {
                Id = existingBook.Id,
                Title = existingBook.Title,
                YearOfPublication = existingBook.YearofPublication,
                ISBN = existingBook.ISBN,
                Price = existingBook.Price,
                CategoryId = existingBook.CategoryId
            };

            return Ok(response);
        }

        // DELETE: api/books/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);

            if (existingBook == null)
            {
                return NotFound();
            }

            await _bookRepository.DeleteAsync(existingBook);

            return Ok();
        }
    }
}