using MoneySaver.App.Models;
using MoneySaver.App.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Services
{
    public interface IReportDataService
    {
        Task<List<DataItem>> GetExpensesPerCategoryAsync(FilterModel filter);
    }
}
