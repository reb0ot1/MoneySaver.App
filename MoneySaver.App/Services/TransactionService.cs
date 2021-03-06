using MoneySaver.App.Models;
using MoneySaver.App.Models.Configurations;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneySaver.App.Services
{
    public class TransactionService : ITransactionService
    {
        private string baseUrl;
        //TODO: Change HttpClient with httpClientFactory
        private HttpClient httpClient { get; set; }

        public TransactionService(HttpClient httpClient, DataApi dataApi)
        {
            this.httpClient = httpClient;
            this.baseUrl = dataApi.Url + "api/transaction";
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            IEnumerable<Transaction> result = new List<Transaction>();
            result = await httpClient
                .GetFromJsonAsync<IEnumerable<Transaction>>(baseUrl);

            return result;
        }

        public async Task<Transaction> AddAsync(Transaction transaction)
        {
            var transactionJson = new StringContent(
                JsonSerializer.Serialize(transaction),
                Encoding.UTF8, "application/json"
                );

            var response = await this.httpClient.PostAsync(baseUrl, transactionJson);
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Transaction>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            var transactionJson = new StringContent(JsonSerializer.Serialize(transaction), Encoding.UTF8, "application/json");

            await this.httpClient.PutAsync(baseUrl, transactionJson);
        }

        public async Task DeleteAsync(string transactionId)
        {
            await this.httpClient.GetAsync($"{baseUrl}/remove/{transactionId}");
        }
    }
}
