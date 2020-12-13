
using Microsoft.AspNetCore.Components;
using MoneySaver.App.Models;
using MoneySaver.App.Pages;
using MoneySaver.App.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Components
{
    public partial class AddTransactionDialog
    {
        public Transaction Transaction { get; set; }
            = new Transaction
            {
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.Now,
            };

        public bool ShowDialog { get; set; }

        public bool ForUpdate { get; set; } = false;

        protected string CategoryId = string.Empty;

        [Inject]
        public ITransactionService TransactionService { get; set; }

        [Parameter]
        public TransactionCategory[] TransactionCategories { get; set; }

        [Parameter]
        public EventCallback<bool> SaveTransactionCallback { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public void Show()
        {
            ResetDialog();
            this.CategoryId = this.TransactionCategories.First().TransactionCategoryId.ToString();
            this.ShowDialog = true;
            StateHasChanged();
        }

        public void ShowForUpdate(Transaction transaction)
        {
            ResetDialog();
            ForUpdate = true;
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
                Id = Guid.NewGuid(),
                TransactionDate = DateTime.Now,
                TransactionCategoryId = TransactionCategories.First().TransactionCategoryId
            };
        }

        protected async Task HandleValidSubmit()
        {
            ShowDialog = false;
            this.Transaction.TransactionCategoryId = int.Parse(CategoryId);
            if (ForUpdate)
            {
                await this.TransactionService.UpdateAsync(this.Transaction);
            }
            else
            {
                await this.TransactionService.AddAsync(this.Transaction);
            }
            
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }   
}
