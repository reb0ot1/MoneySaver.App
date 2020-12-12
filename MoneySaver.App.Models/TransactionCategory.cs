using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySaver.App.Models
{
    public class TransactionCategory
    {
        public int TransactionCategoryId { get; set; }

        public string Name { get; set; }

        public IEnumerable<TransactionCategory> Children { get; set; } = new List<TransactionCategory>();
    }
}
