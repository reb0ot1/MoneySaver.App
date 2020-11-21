using MoneySaver.App.Components;
using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Pages
{
    public partial class Budget
    {
        public const int levelLow = 20;
        public const int levelMiddle = 60;
        public BudgetModel BudgetModel { get; set; } = new BudgetModel();

        public IEnumerable<TransactionCategory> categories = new List<TransactionCategory>()
        {
            new TransactionCategory{ TransactionCategoryId = 1, Name = "Category1"},
            new TransactionCategory{ TransactionCategoryId = 2, Name = "Category2"},
            new TransactionCategory{ TransactionCategoryId = 3, Name = "Category3"},
            new TransactionCategory{ TransactionCategoryId = 4, Name = "Category4"},
            new TransactionCategory{ TransactionCategoryId = 5, Name = "Category5"},
        };

        protected async override Task OnInitializedAsync()
        {
            BudgetModel = new BudgetModel
            {
                BudgetItems = new List<BudgetItemModel>()
                {
                    new BudgetItemModel { BudgetItemId = 1, LimitAmount = 1000, SpentAmount = 345.30, TransactionCategoryId = 1, Progress=34, TransactionCategory = this.categories.FirstOrDefault(f => f.TransactionCategoryId == 1)},
                    new BudgetItemModel { BudgetItemId = 2, LimitAmount = 730, SpentAmount = 25.30, TransactionCategoryId = 2, Progress= 73, TransactionCategory = this.categories.FirstOrDefault(f => f.TransactionCategoryId == 2)},
                    new BudgetItemModel { BudgetItemId = 3, LimitAmount = 34, SpentAmount = 17.60, TransactionCategoryId = 3, Progress = 57, TransactionCategory = this.categories.FirstOrDefault(f => f.TransactionCategoryId == 3)},
                    new BudgetItemModel { BudgetItemId = 4, LimitAmount = 450, SpentAmount = 480.30, TransactionCategoryId = 4, Progress = 80, TransactionCategory = this.categories.FirstOrDefault(f => f.TransactionCategoryId == 4)},
                    new BudgetItemModel { BudgetItemId = 5, LimitAmount = 900, SpentAmount = 1100, TransactionCategoryId = 5, Progress = 90, TransactionCategory = this.categories.FirstOrDefault(f => f.TransactionCategoryId == 5)}
                }
            };
        }

        protected BudgetItemDialog BudgetItemDialog { get; set; }

        protected void AddItem()
        {
            this.BudgetItemDialog.Show();
        }

        public async void AddItem_OnDialogClose(BudgetItemModel budgetItem)
        {
            var newInstance = new BudgetModel()
            {
                BudgetItems = new List<BudgetItemModel>(this.BudgetModel.BudgetItems)
            };
            //var items = new List<BudgetItemModel>(this.BudgetModel.BudgetItems);
            newInstance.BudgetItems.Add(budgetItem);

            this.BudgetModel = newInstance;
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
