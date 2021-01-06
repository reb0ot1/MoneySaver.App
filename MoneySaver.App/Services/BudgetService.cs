using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneySaver.App.Services
{
    public class BudgetService : IBudgetService
    {
        private HttpClient httpClient;
        private Uri uri;
        public BudgetService(HttpClient httpClient)
        {
            this.uri = new Uri("https://localhost:6001");
            this.httpClient = httpClient;
        }
        public async Task<BudgetModel> GetBudgetByTimeType(int intType)
        {
            BudgetModel result = null;
            var uri = new Uri(this.uri, "api/budget/items");
            result = await httpClient.GetFromJsonAsync<BudgetModel>(uri);

            return result;
        }

        public async Task AddBudgetItem(BudgetItemModel budgetItem)
        {
            var budgetItemJson = new StringContent(
                JsonSerializer.Serialize(budgetItem),
                Encoding.UTF8, "application/json"
                );

            var response = await this.httpClient.PostAsync(new Uri(this.uri, "api/budget/additem"), budgetItemJson);
            if (response.IsSuccessStatusCode)
            {
                var result = await JsonSerializer.DeserializeAsync<BudgetItemModel>(await response.Content.ReadAsStreamAsync());
            }
        }

        public async Task UpdateBudgetItem(BudgetItemModel budgetItem)
        {
            var budgetItemJson = new StringContent(
                JsonSerializer.Serialize(budgetItem),
                Encoding.UTF8, "application/json"
                );

            var response = await this.httpClient.PutAsync(new Uri(this.uri, "api/budget/updateitem"), budgetItemJson);
            if (response.IsSuccessStatusCode)
            {
                var result = await JsonSerializer.DeserializeAsync<BudgetItemModel>(await response.Content.ReadAsStreamAsync());
            }
        }
    }
}
