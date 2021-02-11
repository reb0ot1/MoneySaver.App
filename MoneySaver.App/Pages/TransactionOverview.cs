using MoneySaver.App.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MoneySaver.App.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using MoneySaver.App.Services;
using System.Threading;

namespace MoneySaver.App.Pages
{
    public partial class TransactionOverview
    {
        public IEnumerable<Transaction> Transactions { get; set; }

        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public ITransactionService TransactionService { get; set; }

        public IEnumerable<TransactionCategory> TransactionCategories { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var result = await CategoryService.GetAllAsync();
            TransactionCategories = this.PrepareForVisualization(result);
            //TransactionCategories = await CategoryService.GetAllAsync();
            Transactions = (await this.TransactionService.GetAllAsync())
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }

        protected TransactionDialog TransactionDialog { get; set; }

        protected void AddTransaction()
        {
            TransactionDialog.Show();
        }

        protected void UpdateTransaction(Guid transactionId)
        {
            TransactionDialog.Show(this.Transactions.FirstOrDefault(f => f.Id == transactionId));
        }

        public async void AddTransactionDialog_OnDialogClose(bool result)
        {
            this.Transactions = (await this.TransactionService.GetAllAsync()).OrderByDescending(t => t.TransactionDate)
                .ToList();
            StateHasChanged();
        }

        private IEnumerable<TransactionCategory> PrepareForVisualization(IEnumerable<TransactionCategory> categories)
        {
            var result = new List<TransactionCategory>();
            var parentTransactionCategoryModels = categories
                .Where(w => w.ParentId == null);

            foreach (var parentCategory in parentTransactionCategoryModels)
            {
                var children = categories
                    .Where(w => w.ParentId == parentCategory.TransactionCategoryId)
                    .ToList();
                if (children.Any())
                {
                    foreach (var item in children)
                    {
                        item.AlternativeName = $"{parentCategory.Name}, {item.Name}";
                    }
                }

                parentCategory.AlternativeName = parentCategory.Name;
            }

            return categories.OrderBy(e => e.AlternativeName).ToList();
        }
    }
}
