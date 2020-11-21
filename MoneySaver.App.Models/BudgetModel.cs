using System;
using System.Collections.Generic;
using System.Text;

namespace MoneySaver.App.Models
{
    public class BudgetModel
    {
        public int Id { get; set; }

        //public BudgetType Type { get; set; }

        public IList<BudgetItemModel> BudgetItems { get; set; }

        public double LimitAmount { get; set; }
    }
}
