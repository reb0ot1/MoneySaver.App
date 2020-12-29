using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MoneySaver.App.Services
{
    public class BudgetService : IBudgetService
    {
        private HttpClient httpClient;

        public BudgetService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<BudgetModel> GetBudgetByTimeType(int intType)
        {
            BudgetModel result = null;
            result = await httpClient.GetFromJsonAsync<BudgetModel>("https://localhost:6001/api/budget/items");

            return result;
        }
    }
}
