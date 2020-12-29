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
            TransactionCategories = (await CategoryService.GetAll())
                .ToList();
            Transactions = (await this.TransactionService.GetAllAsync())
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }

        protected AddTransactionDialog AddTransactionDialog { get; set; }

        protected void AddTransaction()
        {
            AddTransactionDialog.Show();
        }

        protected void UpdateTransaction(Guid transactionId)
        {
            AddTransactionDialog.ShowForUpdate(this.Transactions.FirstOrDefault(f => f.Id == transactionId));
        }

        public async void AddTransactionDialog_OnDialogClose(bool result)
        {
            this.Transactions = (await this.TransactionService.GetAllAsync()).OrderByDescending(t => t.TransactionDate).ToList();
            StateHasChanged();
        }
    }
}
