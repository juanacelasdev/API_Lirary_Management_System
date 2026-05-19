using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Entities;

namespace Library.Application.Interfaces.Repositories
{
    public interface IBookCopyRepository
    {
        Task<BookCopy?> GetByIdAsync(int id);

        Task<IEnumerable<BookCopy>> GetAvailableCopiesAsync();

        Task<IEnumerable<BookCopy>> GetByBookIdAsync(int bookId);

        Task AddAsync(BookCopy bookCopy);

        void Update(BookCopy bookCopy);

        void Delete(BookCopy bookCopy);
    }
}
