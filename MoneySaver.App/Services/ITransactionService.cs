using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Services
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<Transaction> AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);

        Task DeleteAsync(Transaction transaction);
    }
}
