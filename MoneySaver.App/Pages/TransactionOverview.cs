using MoneySaver.App.Components;
using MoneySaver.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace MoneySaver.App.Pages
{
    public partial class TransactionOverview
    {
        public IEnumerable<Transaction> Transactions { get; set; }

        public IEnumerable<TransactionCategory> TransactionCategories { get; set; }
        protected async override Task OnInitializedAsync()
        {
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
                    Id = "Test1Id",
                    AdditionalNote = "Second interaction",
                    Amount = 34.43,
                    TransactionDate = DateTime.Now,
                    TransactionCategoryId = 1
                }

            };

            TransactionCategories = new List<TransactionCategory>()
            {
                new TransactionCategory{ TransactionCategoryId = 1, Name = "Category1"},
                new TransactionCategory{ TransactionCategoryId = 2, Name = "Category2"},
                new TransactionCategory{ TransactionCategoryId = 3, Name = "Category3"},
            };
        }

        protected AddTransactionDialog AddTransactionDialog { get; set; }

        protected void QuickAddTransaction()
        {
            AddTransactionDialog.Show();
        }

        public async void AddTransactionDialog_OnDialogClose(Transaction transaction)
        {
            //Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            this.Transactions = new List<Transaction>(this.Transactions) { new Transaction { 
                Id = transaction.Id,
                AdditionalNote = transaction.AdditionalNote,
                Amount = transaction.Amount,
                TransactionCategoryId = transaction.TransactionCategoryId,
                TransactionDate = transaction.TransactionDate
            } }
                                    .OrderByDescending(e => e.TransactionDate);
            StateHasChanged();
        }
    }
}
