using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoneySaver.App.Services
{
    public class CategoryService : ICategoryService
    {
        private HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<TransactionCategory>> GetAll()
        {
            IEnumerable<TransactionCategory> result = new List<TransactionCategory>();
            result = await httpClient.GetFromJsonAsync<IEnumerable<TransactionCategory>>("https://localhost:6001/api/category");

            return result;
        }
    }
}
