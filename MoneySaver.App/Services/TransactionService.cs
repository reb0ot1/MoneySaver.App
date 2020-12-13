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
    public class TransactionService : ITransactionService
    {
        private const string baseUrl = "https://localhost:6001/api/transaction";
        //TODO: Change HttpClient with httpClientFactory
        private HttpClient httpClient { get; set; }

        public TransactionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            IEnumerable<Transaction> result = new List<Transaction>();
            //TODO: Move the url in a centralize place
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

        public async Task DeleteAsync(Transaction transaction)
        {
            await this.httpClient.DeleteAsync(baseUrl);
        }
    }
}
