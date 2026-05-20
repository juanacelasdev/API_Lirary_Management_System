using Library.Application.DTOs.Features.Loans.Commands;
using Library.Application.DTOs.Features.Loans.DTOs;
using Library.Application.DTOs.Features.Loans.Queries;
using Library.Application.Interfaces.Repositories;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API_library_management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;

        public LoansController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        // GET: api/loans
        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            var query = new GetAllLoansQuery();

            var loans = await _loanRepository.GetAllAsync();

            var response = loans.Select(loan => new LoanDto
            {
                Id = loan.Id,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = (int)loan.Status,
                UserId = loan.UserId,
                BookCopyId = loan.BookCopyId
            });

            return Ok(response);
        }

        // GET: api/loans/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            var query = new GetLoanByIdQuery(id);

            var loan = await _loanRepository.GetByIdAsync(query.Id);

            if (loan == null)
            {
                return NotFound();
            }

            var response = new LoanDto
            {
                Id = loan.Id,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = (int)loan.Status,
                UserId = loan.UserId,
                BookCopyId = loan.BookCopyId
            };

            return Ok(response);
        }

        // GET: api/loans/user/1
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLoansByUserId(int userId)
        {
            var query = new GetLoansByUserIdQuery(userId);

            var loans = await _loanRepository.GetLoansByUserIdAsync(query.UserId);

            var response = loans.Select(loan => new LoanDto
            {
                Id = loan.Id,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = (int)loan.Status,
                UserId = loan.UserId,
                BookCopyId = loan.BookCopyId
            });

            return Ok(response);
        }

        // GET: api/loans/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveLoans()
        {
            var query = new GetActiveLoansQuery();

            var loans = await _loanRepository.GetActiveLoansAsync();

            var response = loans.Select(loan => new LoanDto
            {
                Id = loan.Id,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = (int)loan.Status,
                UserId = loan.UserId,
                BookCopyId = loan.BookCopyId
            });

            return Ok(response);
        }

        // GET: api/loans/overdue
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverdueLoans()
        {
            var query = new GetOverdueLoansQuery();

            var loans = await _loanRepository.GetOverdueLoansAsync();

            var response = loans.Select(loan => new LoanDto
            {
                Id = loan.Id,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = (int)loan.Status,
                UserId = loan.UserId,
                BookCopyId = loan.BookCopyId
            });

            return Ok(response);
        }

        // POST: api/loans
        [HttpPost]
        public async Task<IActionResult> CreateLoan(CreateLoanCommand command)
        {
            var loan = new Loan
            {
                LoanDate = command.LoanDate,
                DueDate = command.DueDate,
                ReturnDate = (DateTime)command.ReturnDate,
                Status = (LoadStatus)command.Status,
                UserId = command.UserId,
                BookCopyId = command.BookCopyId
            };

            await _loanRepository.AddAsync(loan);

            var response = new LoanDto
            {
                Id = loan.Id,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = (int)loan.Status,
                UserId = loan.UserId,
                BookCopyId = loan.BookCopyId
            };

            return Ok(response);
        }

        // PUT: api/loans/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, UpdateLoanCommand command)
        {
            var existingLoan = await _loanRepository.GetByIdAsync(id);

            if (existingLoan == null)
            {
                return NotFound();
            }

            existingLoan.LoanDate = command.LoanDate;
            existingLoan.DueDate = command.DueDate;
            existingLoan.ReturnDate = (DateTime)command.ReturnDate;
            existingLoan.Status = (LoadStatus)command.Status;
            existingLoan.UserId = command.UserId;
            existingLoan.BookCopyId = command.BookCopyId;

            await _loanRepository.UpdateAsync(existingLoan);

            var response = new LoanDto
            {
                Id = existingLoan.Id,
                LoanDate = existingLoan.LoanDate,
                DueDate = existingLoan.DueDate,
                ReturnDate = existingLoan.ReturnDate,
                Status = (int)existingLoan.Status,
                UserId = existingLoan.UserId,
                BookCopyId = existingLoan.BookCopyId
            };

            return Ok(response);
        }

        // DELETE: api/loans/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var existingLoan = await _loanRepository.GetByIdAsync(id);

            if (existingLoan == null)
            {
                return NotFound();
            }

            await _loanRepository.DeleteAsync(existingLoan);

            return Ok();
        }
    }
}