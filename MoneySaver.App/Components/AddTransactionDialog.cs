
using Microsoft.AspNetCore.Components;
using MoneySaver.Shared.Models;
using System;
using System.Threading.Tasks;

namespace MoneySaver.App.Components
{
    public partial class AddTransactionDialog
    {
        public Transaction Transaction { get; set; } 
            = new Transaction { AdditionalNote = "Test note",  CreateOn = DateTime.Now, Amount = 0.0, TransactionCategoryId = 1};

        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public void Show()
        {
            ResetDialog();
            this.ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            this.ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        { 
            
        }

        protected async Task HandleValidSubmit()
        {
            //await EmployeeDataService.AddEmployee(Employee);
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }   
}
