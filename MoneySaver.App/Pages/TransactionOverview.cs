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

        public IEnumerable<TransactionCategory> TransactionCategories { get; set; }

        protected async override Task OnInitializedAsync()
        {
            TransactionCategories = (await CategoryService.GetAll()).ToList();

            Transactions = new List<Transaction>()
            {
                new Transaction{
                    Id = "Test1Id",
                    AdditionalNote = "First interaction",
                    Amount = 30.40,
                    TransactionDate = DateTime.Now,
                    TransactionCategoryId = 1
                },
                new Transaction{
                    Id = "Test2Id",
                    AdditionalNote = "Second interaction",
                    Amount = 34.43,
                    TransactionDate = DateTime.Now,
                    TransactionCategoryId = 1
                }
            };

            //var ttt = (await CategoryService.GetAll()).ToList();
            //TransactionCategories = new List<TransactionCategory>()
            //{
            //    new TransactionCategory{ TransactionCategoryId = 1, Name = "Category1"},
            //    new TransactionCategory{ TransactionCategoryId = 2, Name = "Category2"},
            //    new TransactionCategory{ TransactionCategoryId = 3, Name = "Category3"},
            //};
        }

        protected AddTransactionDialog AddTransactionDialog { get; set; }

        protected void AddTransaction()
        {
            AddTransactionDialog.Show();
        }

        protected void UpdateTransaction(string transactionId)
        {
            AddTransactionDialog.ShowForUpdate(this.Transactions.FirstOrDefault(f => f.Id == transactionId));
        }

        public async void AddTransactionDialog_OnDialogClose(Transaction transaction)
        {
            //Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            var transactions = new List<Transaction>(this.Transactions);
            var transactionExists = transactions.FirstOrDefault(c => c.Id == transaction.Id);
            var addOrUpdateTransaction = new Transaction();
            if (transactionExists != null)
            {
                addOrUpdateTransaction = transactionExists;
            }

            addOrUpdateTransaction.Id = transaction.Id;
            addOrUpdateTransaction.AdditionalNote = transaction.AdditionalNote;
            addOrUpdateTransaction.Amount = transaction.Amount;
            addOrUpdateTransaction.TransactionCategoryId = transaction.TransactionCategoryId;
            addOrUpdateTransaction.TransactionDate = transaction.TransactionDate;
            
            this.Transactions = transactions.OrderByDescending(e => e.TransactionDate);
            StateHasChanged();
        }
    }
}
