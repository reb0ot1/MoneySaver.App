using Microsoft.AspNetCore.Components;
using MoneySaver.App.Components;
using MoneySaver.App.Models;
using MoneySaver.App.Services;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Pages
{
    public partial class Configuration
    {
        public IEnumerable<TransactionCategory> Categories { get; set; }

        [Inject]
        public ICategoryService categoryService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            //TODO: filtration needs refactoring
            var result = (await this.categoryService.GetAllAsync());
            this.Categories = this.PrepareForVisualization(result);
        }

            //EventConsole console;
            void Log(string eventName, string value)
        {
            //console.Log($"{eventName}: {value}");
        }
        void OnChange(TreeEventArgs args)
        {
            Log("Change", $"Item Text: {args.Text}");
        }

        void OnExpand(TreeExpandEventArgs args)
        {
            //if (args.Value is Category category)
            //{
            //    Log("Expand", $"Text: {category.CategoryName}");
            //}

            //if (args.Value is string text)
            //{
            //    Log("Expand", $"Text: {text}");
            //}
        }

        public async void OnDialogClose()
        {
            //TODO: Needs refactoring
            //TODO: filtration needs refactoring
            var result = (await this.categoryService.GetAllAsync());
            this.Categories = this.PrepareForVisualization(result);

            StateHasChanged();
        }

        protected CategoryDialog CategoryDialog { get; set; }

        protected void AddItem(TransactionCategory item = null)
        {
            if (item != null)
            {
                if (item.ParentId == null)
                {
                    var newItem = new TransactionCategory();
                    newItem.ParentId = item.TransactionCategoryId;


                    this.CategoryDialog.Show(newItem);
                }
            }
            else
            {
                this.CategoryDialog.Show(item);
            }
        }

        protected void EditItem(TransactionCategory parrentItem)
        {
            this.CategoryDialog.Show(parrentItem);
        }

        protected void DeleteItem(TransactionCategory parrentItem)
        {

        }

        private List<TransactionCategory> PrepareForVisualization(IEnumerable<TransactionCategory> categories)
        {
            var parentTransactionCategoryModels = categories
                .Where(w => w.ParentId == null)
                .Select(s => new TransactionCategory
                {
                    TransactionCategoryId = s.TransactionCategoryId,
                    Name = s.Name,
                    AlternativeName = s.Name
                })
                .ToList();

            foreach (var parentCategory in parentTransactionCategoryModels)
            {
                var children = categories
                    .Where(w => w.ParentId == parentCategory.TransactionCategoryId);

                if (children.Any())
                {
                    parentCategory.Children = children
                        .Select(s => new TransactionCategory
                        {
                            Name = s.Name,
                            TransactionCategoryId = s.TransactionCategoryId,
                            ParentId = s.ParentId,
                            AlternativeName = $"{parentCategory.Name}, {s.Name}"
                        })
                        .OrderBy(e => e.AlternativeName)
                        .ToList();
                }
            }

            return parentTransactionCategoryModels.OrderBy(e => e.AlternativeName).ToList();
        }
    }
}
