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

        public async void AddItem_OnDialogClose(BudgetItemModel budgetItem)
        {
            //var newInstance = new BudgetModel()
            //{
            //    BudgetItems = new List<BudgetItemModel>(this.BudgetModel.BudgetItems)
            //};
            ////var items = new List<BudgetItemModel>(this.BudgetModel.BudgetItems);
            //newInstance.BudgetItems.Add(budgetItem);

            //this.BudgetModel = newInstance;
            //var transactionExists = transactions.FirstOrDefault(c => c.Id == transaction.Id);
            //var addOrUpdateTransaction = new Transaction();
            //if (transactionExists != null)
            //{
            //    addOrUpdateTransaction = transactionExists;
            //}

            //addOrUpdateTransaction.Id = transaction.Id;
            //addOrUpdateTransaction.AdditionalNote = transaction.AdditionalNote;
            //addOrUpdateTransaction.Amount = transaction.Amount;
            //addOrUpdateTransaction.TransactionCategoryId = transaction.TransactionCategoryId;
            //addOrUpdateTransaction.TransactionDate = transaction.TransactionDate;

            //this.Transactions = transactions.OrderByDescending(e => e.TransactionDate);
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
