using Microsoft.AspNetCore.Components;
using MoneySaver.App.Components;
using MoneySaver.App.Models;
using MoneySaver.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Pages
{
    public partial class Budget
    {
        //TODO: Move these constants somewhere else
        public const int levelLow = 20;
        public const int levelMiddle = 60;

        [Inject]
        public ICategoryService CategoryService { get; set; }

        [Inject]
        public IBudgetService BudgetService { get; set; }

        public BudgetModel BudgetModel { get; set; }

        public IEnumerable<TransactionCategory> TransactionCategories = new List<TransactionCategory>();

        protected async override Task OnInitializedAsync()
        {
            TransactionCategories = (await CategoryService.GetAll())
                .ToList();

            var budgetItems = await BudgetService.GetBudgetByTimeType(2);
            foreach (var item in budgetItems.BudgetItems)
            {
                if (item != null)
                {
                    item.TransactionCategory = this.TransactionCategories
                        .FirstOrDefault(e => e.TransactionCategoryId == item.TransactionCategoryId);
                }
            }

            BudgetModel = budgetItems;
        }

        protected BudgetItemDialog BudgetItemDialog { get; set; }

        protected void AddItem()
        {
            this.BudgetItemDialog.Show();
        }

        protected void EditItem(BudgetItemModel item)
        {
            this.BudgetItemDialog.Show(item);
        }

        public async void AddItem_OnDialogClose()
        {
            TransactionCategories = (await CategoryService.GetAll())
                .ToList();

            var budgetItems = await BudgetService.GetBudgetByTimeType(2);
            foreach (var item in budgetItems.BudgetItems)
            {
                if (item != null)
                {
                    item.TransactionCategory = this.TransactionCategories
                        .FirstOrDefault(e => e.TransactionCategoryId == item.TransactionCategoryId);
                }
            }

            BudgetModel = budgetItems;
            StateHasChanged();
        }

        public static string CheckLevel(int percValue)
        {
            if (levelLow < percValue && percValue <= levelMiddle)
            {
                return "bg-warning";
            }

            if (percValue <= levelLow)
            {
                return "bg-danger";
            }

            return "bg-success";
        }
    }
}
