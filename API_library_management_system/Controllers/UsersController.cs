using Library.Application.DTOs.Features.Users.Commands;
using Library.Application.DTOs.Features.Users.DTOs;
using Library.Application.DTOs.Features.Users.Queries;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_library_management_system.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsersQuery();

            var users = await _userRepository.GetAllAsync();

            var response = users.Select(user => new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Role = (int)user.Role,
                IsActive = user.IsActive
            });

            return Ok(response);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetUserByIdQuery(id);

            var user = await _userRepository.GetByIdAsync(query.Id);

            if (user == null)
            {
                return NotFound();
            }

            var response = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Role = (int)user.Role,
                IsActive = user.IsActive
            };

            return Ok(response);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var user = new User
            {
                Name = command.Name,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password,
                Role = (Library.Domain.Enums.UserRole)command.Role,
                IsActive = command.IsActive
            };

            await _userRepository.AddAsync(user);

            var response = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Role = (int)user.Role,
                IsActive = user.IsActive
            };

            return Ok(response);
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = command.Name;
            existingUser.LastName = command.LastName;
            existingUser.Email = command.Email;
            existingUser.Password = command.Password;
            existingUser.Role = (Library.Domain.Enums.UserRole)command.Role;
            existingUser.IsActive = command.IsActive;

            await _userRepository.UpdateAsync(existingUser);

            var response = new UserDto
            {
                Id = existingUser.Id,
                Name = existingUser.Name,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                Role = (int)existingUser.Role,
                IsActive = existingUser.IsActive
            };

            return Ok(response);
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(existingUser);

            return Ok();
        }
    }
}