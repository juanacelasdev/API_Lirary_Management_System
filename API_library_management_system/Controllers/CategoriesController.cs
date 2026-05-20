using Library.Application.DTOs.Features.Categories.Commands;
using Library.Application.DTOs.Features.Categories.DTOs;
using Library.Application.DTOs.Features.Categories.Queries;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API_library_management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var query = new GetAllCategoriesQuery();

            var categories = await _categoryRepository.GetAllAsync();

            var response = categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            });

            return Ok(response);
        }

        // GET: api/categories/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var query = new GetCategoryByIdQuery(id);

            var category = await _categoryRepository.GetByIdAsync(query.Id);

            if (category == null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };

            return Ok(response);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var category = new Category
            {
                Name = command.Name,
                Description = command.Description,
                IsActive = command.IsActive
            };

            await _categoryRepository.AddAsync(category);

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            };

            return Ok(response);
        }

        // PUT: api/categories/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryCommand command)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = command.Name;
            existingCategory.Description = command.Description;
            existingCategory.IsActive = command.IsActive;

            await _categoryRepository.UpdateAsync(existingCategory);

            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                Description = existingCategory.Description,
                IsActive = existingCategory.IsActive
            };

            return Ok(response);
        }

        // DELETE: api/categories/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(existingCategory);

            return Ok();
        }
    }
}