using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<TransactionCategory>> GetAll();
    }
}
