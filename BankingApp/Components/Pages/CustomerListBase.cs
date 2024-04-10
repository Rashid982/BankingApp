using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Components;

namespace BankingApp.Components.Pages
{
    public class CustomerListBase : ComponentBase
    {
        [Inject]
        public ICustomerService CustomerService { get; set; }

        public IEnumerable<Customer> Customers { get; set; }

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Customers = (await CustomerService.GetCustomers()).ToList() ;
        }

        protected async Task DeleteCustomer(int id)
        {
            await CustomerService.DeleteCustomer(id);
            NavigationManager.NavigateTo("/", true);            
        }
    }
}
