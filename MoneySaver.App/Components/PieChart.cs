using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MoneySaver.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaver.App.Components
{
    public partial class PieChart
    {
        [Inject]
        private IJSRuntime jsRuntimeService { get; set; }

        [Parameter]
        public DataItem[] Data { get; set; }

        [Parameter]
        public string ChartName { get; set; }

        [Parameter]
        public string ChartContainer { get; set; }

        public bool Update { get; set; } = false;

        protected override bool ShouldRender()
        {
            return this.Update;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await this.jsRuntimeService.InvokeVoidAsync(
                "pieChart.showChart", this.Data, this.ChartContainer);

            this.Update = false;
        }
    }
}
