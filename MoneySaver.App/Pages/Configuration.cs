using MoneySaver.App.Models;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Pages
{
    public partial class Configuration
    {
        IEnumerable<TransactionCategory> categories;

        protected override void OnInitialized()
        {
            this.categories = new List<TransactionCategory> {
                new TransactionCategory { Name = "Test1", TransactionCategoryId = 1, Children = new List<TransactionCategory>{ new TransactionCategory { TransactionCategoryId = 2, Name = "Tr1"} } },
                new TransactionCategory { Name = "Test2", TransactionCategoryId = 2, Children = new List<TransactionCategory>{ new TransactionCategory { TransactionCategoryId = 2, Name = "Tr1"} }},
                new TransactionCategory { Name = "Test3", TransactionCategoryId = 3}
            };

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
    }
}
