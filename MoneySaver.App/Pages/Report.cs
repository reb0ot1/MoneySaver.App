using Microsoft.AspNetCore.Components;
using MoneySaver.App.Components;
using MoneySaver.App.Models;
using MoneySaver.App.Models.Filters;
using MoneySaver.App.Services;
using System.Threading.Tasks;

namespace MoneySaver.App.Pages
{
    public partial class Report
    {
        public DataItem[] Data { get; set; }

        public FilterModel Filter { get; set; }

        [Inject]
        public IReportDataService reportDataService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.Filter = new FilterModel();
               //TODO: filtration needs refactoring
            var result = await this.reportDataService.GetExpensesPerCategoryAsync(this.Filter);
            this.Data = result.ToArray();
        }

        protected PieChart PieChart { get; set; }

        protected LineChart LineChart { get; set; }

        protected async Task HandleValidSubmit()
        {
            var result = await this.reportDataService.GetExpensesPerCategoryAsync(this.Filter);
            this.Data = result.ToArray();

            PieChart.Update = true;
        }
    }
}
