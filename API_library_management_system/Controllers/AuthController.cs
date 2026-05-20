using Library.Application.DTOs.Features.Auth.DTOs;
using Library.Application.Interfaces.Repositories;
using Library.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API_library_management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(
            IUserRepository userRepository,
            JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var users = await _userRepository.GetAllAsync();

            var user = users.FirstOrDefault(u =>
                u.Email == loginDto.Email &&
                u.Password == loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Correo o contraseña incorrectos");
            }

            var token = _jwtService.GenerateToken(user);

            var response = new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            };

            return Ok(response);
        }
    }
}