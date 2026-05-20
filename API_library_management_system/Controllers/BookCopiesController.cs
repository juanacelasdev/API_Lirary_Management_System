using Library.Application.DTOs.Features.BookCopies.Commands;
using Library.Application.DTOs.Features.BookCopies.DTOs;
using Library.Application.DTOs.Features.BookCopies.Queries;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API_library_management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookCopiesController : ControllerBase
    {
        private readonly IBookCopyRepository _bookCopyRepository;

        public BookCopiesController(IBookCopyRepository bookCopyRepository)
        {
            _bookCopyRepository = bookCopyRepository;
        }

        // GET: api/bookcopies
        [HttpGet]
        public async Task<IActionResult> GetAllBookCopies()
        {
            var query = new GetAllBookCopiesQuery();

            var copies = await _bookCopyRepository.GetAllAsync();

            var response = copies.Select(copy => new BookCopyDto
            {
                Id = copy.Id,
                InventoryCode = copy.InventoryCode,
                Status = copy.Status.ToString(),
                IsActive = copy.IsActive,
                BookId = copy.BookId
            });

            return Ok(response);
        }

        // GET: api/bookcopies/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookCopyById(int id)
        {
            var query = new GetBookCopyByIdQuery(id);

            var copy = await _bookCopyRepository.GetByIdAsync(query.Id);

            if (copy == null)
            {
                return NotFound();
            }

            var response = new BookCopyDto
            {
                Id = copy.Id,
                InventoryCode = copy.InventoryCode,
                Status = copy.Status.ToString(),
                IsActive = copy.IsActive,
                BookId = copy.BookId
            };

            return Ok(response);
        }

        // GET: api/bookcopies/available
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBookCopies()
        {
            var query = new GetAvailableBookCopiesQuery();

            var copies = await _bookCopyRepository.GetAvailableCopiesAsync();

            var response = copies.Select(copy => new BookCopyDto
            {
                Id = copy.Id,
                InventoryCode = copy.InventoryCode,
                Status = copy.Status.ToString(),
                IsActive = copy.IsActive,
                BookId = copy.BookId
            });

            return Ok(response);
        }

        // GET: api/bookcopies/book/1
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetBookCopiesByBookId(int bookId)
        {
            var query = new GetBookCopiesByBookIdQuery(bookId);

            var copies = await _bookCopyRepository
                .GetByBookIdAsync(query.BookId);

            var response = copies.Select(copy => new BookCopyDto
            {
                Id = copy.Id,
                InventoryCode = copy.InventoryCode,
                Status = copy.Status.ToString(),
                IsActive = copy.IsActive,
                BookId = copy.BookId
            });

            return Ok(response);
        }

        // POST: api/bookcopies
        [HttpPost]
        public async Task<IActionResult> CreateBookCopy(
            CreateBookCopyCommand command)
        {
            var copy = new BookCopy
            {
                InventoryCode = command.InventoryCode,
                Status = (BookCopyStatus)command.Status,
                IsActive = command.IsActive,
                BookId = command.BookId
            };

            await _bookCopyRepository.AddAsync(copy);

            var response = new BookCopyDto
            {
                Id = copy.Id,
                InventoryCode = copy.InventoryCode,
                Status = copy.Status.ToString(),
                IsActive = copy.IsActive,
                BookId = copy.BookId
            };

            return Ok(response);
        }

        // PUT: api/bookcopies/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookCopy(
            int id,
            UpdateBookCopyCommand command)
        {
            var existingCopy =
                await _bookCopyRepository.GetByIdAsync(id);

            if (existingCopy == null)
            {
                return NotFound();
            }

            existingCopy.InventoryCode = command.InventoryCode;
            existingCopy.Status = (BookCopyStatus)command.Status;
            existingCopy.IsActive = command.IsActive;
            existingCopy.BookId = command.BookId;

            await _bookCopyRepository.UpdateAsync(existingCopy);

            var response = new BookCopyDto
            {
                Id = existingCopy.Id,
                InventoryCode = existingCopy.InventoryCode,
                Status = existingCopy.Status.ToString(),
                IsActive = existingCopy.IsActive,
                BookId = existingCopy.BookId
            };

            return Ok(response);
        }

        // DELETE: api/bookcopies/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCopy(int id)
        {
            var existingCopy =
                await _bookCopyRepository.GetByIdAsync(id);

            if (existingCopy == null)
            {
                return NotFound();
            }

            await _bookCopyRepository.DeleteAsync(existingCopy);

            return Ok();
        }
    }
}