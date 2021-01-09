using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Services
{
    public interface IBudgetService
    {
        Task<BudgetModel> GetBudgetByTimeType(int intType);

        Task AddBudgetItem(BudgetItemModel budgetItem);

        Task UpdateBudgetItem(BudgetItemModel budgetItem);

        Task RemoveBudgetItem(int id);
    }
}
