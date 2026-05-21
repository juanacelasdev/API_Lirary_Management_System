using Library.Application.Features.Authors.Commands;
using Library.Application.Features.Authors.DTOs;
using Library.Application.Features.Authors.Queries;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_library_management_system.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var query = new GetAllAuthorsQuery();

            var authors = await _authorRepository.GetAllAsync();

            var response = authors.Select(author => new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                DateOfBirth = author.DateofBirth
            });

            return Ok(response);
        }

        // GET: api/authors/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var query = new GetAuthorByIdQuery(id);

            var author = await _authorRepository.GetByIdAsync(query.Id);

            if (author == null)
            {
                return NotFound();
            }

            var response = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                LastName = author.LastName,
                DateOfBirth = author.DateofBirth
            };

            return Ok(response);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
        {
            var author = new Author
            {
                Name = command.Name,
                LastName = command.LastName,
                DateofBirth = command.DateOfBirth
            };

            await _authorRepository.AddAsync(author);

            return Ok(author);
        }

        // PUT: api/authors/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorCommand command)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(id);

            if (existingAuthor == null)
            {
                return NotFound();
            }

            existingAuthor.Name = command.Name;
            existingAuthor.LastName = command.LastName;
            existingAuthor.DateofBirth = command.DateOfBirth;

            await _authorRepository.UpdateAsync(existingAuthor);

            return Ok(existingAuthor);
        }

        // DELETE: api/authors/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(id);

            if (existingAuthor == null)
            {
                return NotFound();
            }

            await _authorRepository.DeleteAsync(existingAuthor);

            return Ok();
        }
    }
}