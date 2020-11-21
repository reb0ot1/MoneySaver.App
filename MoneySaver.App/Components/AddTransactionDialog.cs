
using Microsoft.AspNetCore.Components;
using MoneySaver.App.Models;
using MoneySaver.App.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneySaver.App.Components
{
    public partial class AddTransactionDialog
    {
        public Transaction Transaction { get; set; }
            = new Transaction
            {
                Id = Utilities.GenerateRandomString(8),
                TransactionDate = DateTime.Now,
            };

        public bool ShowDialog { get; set; }
        protected string CategoryId = string.Empty;
        [Parameter]
        public TransactionCategory[] TransactionCategories { get; set; }

        [Parameter]
        public EventCallback<bool> SaveTransactionCallback { get; set; }

        [Parameter]
        public EventCallback<Transaction> CloseEventCallback { get; set; }

        public void Show()
        {
            ResetDialog();
            this.ShowDialog = true;
            StateHasChanged();
        }

        public void ShowForUpdate(Transaction transaction)
        {
            ResetDialog();
            this.Transaction = transaction;
            this.CategoryId = transaction.TransactionCategoryId.ToString();
            this.ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            this.ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            this.CategoryId = default;
            this.Transaction = new Transaction
            {
                Id = Utilities.GenerateRandomString(8),
                TransactionDate = DateTime.Now,
            };
        }

        protected async Task HandleValidSubmit()
        {
            ShowDialog = false;
            this.Transaction.TransactionCategoryId = int.Parse(CategoryId);
            await CloseEventCallback.InvokeAsync(this.Transaction);
            StateHasChanged();
        }
    }   
}
