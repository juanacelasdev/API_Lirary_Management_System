using Library.Application.DTOs.Features.Reservations.Commands;
using Library.Application.DTOs.Features.Reservations.DTOs;
using Library.Application.DTOs.Features.Reservations.Queries;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API_library_management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        // GET: api/reservations
        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var query = new GetAllReservationsQuery();

            var reservations = await _reservationRepository.GetAllAsync();

            var response = reservations.Select(reservation => new ReservationDto
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                ExpirationDate = reservation.ExpirationDate,
                Status = reservation.Status.ToString(),
                UserId = reservation.UserId,
                BookId = reservation.BookId
            });

            return Ok(response);
        }

        // GET: api/reservations/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var query = new GetReservationByIdQuery(id);

            var reservation = await _reservationRepository.GetByIdAsync(query.Id);

            if (reservation == null)
            {
                return NotFound();
            }

            var response = new ReservationDto
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                ExpirationDate = reservation.ExpirationDate,
                Status = reservation.Status.ToString(),
                UserId = reservation.UserId,
                BookId = reservation.BookId
            };

            return Ok(response);
        }

        // GET: api/reservations/user/1
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReservationsByUserId(int userId)
        {
            var query = new GetReservationsByUserIdQuery(userId);

            var reservations = await _reservationRepository
                .GetReservationsByUserIdAsync(query.UserId);

            var response = reservations.Select(reservation => new ReservationDto
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                ExpirationDate = reservation.ExpirationDate,
                Status = reservation.Status.ToString(),
                UserId = reservation.UserId,
                BookId = reservation.BookId
            });

            return Ok(response);
        }

        // GET: api/reservations/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveReservations()
        {
            var query = new GetActiveReservationsQuery();

            var reservations = await _reservationRepository
                .GetActiveReservationsAsync();

            var response = reservations.Select(reservation => new ReservationDto
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                ExpirationDate = reservation.ExpirationDate,
                Status = reservation.Status.ToString(),
                UserId = reservation.UserId,
                BookId = reservation.BookId
            });

            return Ok(response);
        }

        // POST: api/reservations
        [HttpPost]
        public async Task<IActionResult> CreateReservation(
            CreateReservationCommand command)
        {
            var reservation = new Reservation
            {
                ReservationDate = command.ReservationDate,
                ExpirationDate = command.ExpirationDate,
                Status = (ReservationStatus)command.Status,
                UserId = command.UserId,
                BookId = command.BookId
            };

            await _reservationRepository.AddAsync(reservation);

            var response = new ReservationDto
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                ExpirationDate = reservation.ExpirationDate,
                Status = reservation.Status.ToString(),
                UserId = reservation.UserId,
                BookId = reservation.BookId
            };

            return Ok(response);
        }

        // PUT: api/reservations/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(
            int id,
            UpdateReservationCommand command)
        {
            var existingReservation =
                await _reservationRepository.GetByIdAsync(id);

            if (existingReservation == null)
            {
                return NotFound();
            }

            existingReservation.ReservationDate =
                command.ReservationDate;

            existingReservation.ExpirationDate =
                command.ExpirationDate;

            existingReservation.Status =
                (ReservationStatus)command.Status;

            existingReservation.UserId = command.UserId;

            existingReservation.BookId = command.BookId;

            await _reservationRepository.UpdateAsync(existingReservation);

            var response = new ReservationDto
            {
                Id = existingReservation.Id,
                ReservationDate = existingReservation.ReservationDate,
                ExpirationDate = existingReservation.ExpirationDate,
                Status = existingReservation.Status.ToString(),
                UserId = existingReservation.UserId,
                BookId = existingReservation.BookId
            };

            return Ok(response);
        }

        // DELETE: api/reservations/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var existingReservation =
                await _reservationRepository.GetByIdAsync(id);

            if (existingReservation == null)
            {
                return NotFound();
            }

            await _reservationRepository.DeleteAsync(id);

            return Ok();
        }
    }
}