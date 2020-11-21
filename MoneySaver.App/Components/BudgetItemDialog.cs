using Microsoft.AspNetCore.Components;
using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Components
{
    public partial class BudgetItemDialog
    {
        public BudgetItemModel BudgetItemModel { get; set; }

        public IEnumerable<TransactionCategory> ТransactionCategories { get; set; }

        [Parameter]
        public EventCallback<BudgetItemModel> CloseEventCallback { get; set; }

        public string CategoryId { get; set; }

        public bool ShowDialog { get; set; } = false;

        protected async Task HandleValidSubmit()
        {
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(this.BudgetItemModel);
            StateHasChanged();
        }

        public void Show()
        {
            ResetDialog();
            this.ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ResetDialog();
            this.ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            this.CategoryId = default;
            this.BudgetItemModel = new BudgetItemModel
            {
                TransactionCategoryId = 1,
                LimitAmount = 0
            };
        }
    }
}
