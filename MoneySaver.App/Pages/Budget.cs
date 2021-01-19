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
            var categories = await CategoryService.GetAllAsync();
            TransactionCategories = this.PrepareForVisualization(categories);

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
            //TODO: Needs refactoring
            var categories = await CategoryService.GetAllAsync();
            TransactionCategories = this.PrepareForVisualization(categories);

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

        public async void RemoveItem(int id)
        {
            //TODO: Needs refactoring
            //TODO: Neet to show pop-up to confirm that the user is okay to remove the selected item
            await this.BudgetService.RemoveBudgetItem(id);
            var categories = await CategoryService.GetAllAsync();
            TransactionCategories = this.PrepareForVisualization(categories);

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

        //TODO: The method bellow needs to be declare once, because it`s used by other pages.
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

            return categories.OrderBy(e => e.AlternativeName);
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
