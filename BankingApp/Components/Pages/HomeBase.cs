using BankingApp.Models;
using Microsoft.AspNetCore.Components;

namespace BankingApp.Components.Pages
{
    public class HomeBase : ComponentBase
    {
        public Customer Customer { get; set; }

        public HomeBase() 
        { 
            Customer = new Customer();
        }
    }
}
