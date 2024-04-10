using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Components;

namespace BankingApp.Components.Pages
{
    public class EditCustomerBase : ComponentBase
    {
        [Inject]
        public ICustomerService CustomerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Customer Customer { get; set; } = new Customer();

        [Parameter]
        public string Id { get; set; }

        public decimal Amount { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Customer = await CustomerService.GetCustomerById(int.Parse(Id));            
        }
                
        protected async Task TopupBalance()
        {
            await CustomerService.TopUpCustomerBalance(int.Parse(Id), Amount);
            NavigationManager.NavigateTo("/");
        }

        protected async Task RefundBalance()
        {
            await CustomerService.Refund(int.Parse(Id), Amount);
            NavigationManager.NavigateTo("/");
        }

        protected async Task PurchaseBalance()
        {
            await CustomerService.Purchase(int.Parse(Id), Amount);
            NavigationManager.NavigateTo("/");
        }

    }
}
