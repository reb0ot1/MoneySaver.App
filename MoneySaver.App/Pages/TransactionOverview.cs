using MoneySaver.App.Components;
using MoneySaver.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MoneySaver.App.Pages
{
    public partial class TransactionOverview
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        protected async override Task OnInitializedAsync()
        {

            Transactions = new List<Transaction>()
            {
                new Transaction{
                    Id = Guid.NewGuid(),
                    AdditionalNote = "First interaction",
                    Amount = 30.40,
                    CreateOn = DateTime.Now,
                    ModifyOn = DateTime.Now,
                    TransactionCategory = new TransactionCategory{ Id = 1, Name = "Category1"}
                },
                new Transaction{
                    Id = Guid.NewGuid(),
                    AdditionalNote = "Second interaction",
                    Amount = 34.43,
                    CreateOn = DateTime.Now,
                    ModifyOn = DateTime.Now,
                    TransactionCategory = new TransactionCategory{ Id = 2, Name = "Category2"}
                }

            };
        }

        protected AddTransactionDialog AddTransactionDialog { get; set; }

        protected void QuickAddTransaction()
        {
            AddTransactionDialog.Show();
        }

        public async void AddTransactionDialog_OnDialogClose()
        {
            //Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            StateHasChanged();
        }
    }
}
