using BankingApp.Models;
using BankingApp.Services;
using Microsoft.AspNetCore.Components;

namespace BankingApp.Components.Pages
{
    public class CreateCustomerBase : ComponentBase
    {
        [Inject]
        public ICustomerService CustomerService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Customer Customer { get; set; } = new Customer();

        protected override async Task OnInitializedAsync()
        {
            Customer.BirthDate = DateTime.Now;
        }

        protected async Task HandleValidSubmit()
        {
            await CustomerService.AddCustomer(Customer);

            NavigationManager.NavigateTo("/");
        }
    }
}
