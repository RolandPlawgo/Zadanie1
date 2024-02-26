using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie1_UI.Models;

namespace Zadanie1_UI.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Customer CurrentCustomer { get; set; } = new Customer();

        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("api");
            var result = await client.GetFromJsonAsync<Customer>($"Customers/{id}");
            if (result is null)
            {
                return RedirectToPage("Error");
            }

            CurrentCustomer = result;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var customer = new Customer();
            customer.Id = CurrentCustomer.Id;
            customer.Name = Request.Form["CustomerName"];
            customer.NIP = Request.Form["NIP"];
            customer.Address = new Address() {
                Street = Request.Form["Street"],
                HouseNumber = Request.Form["HouseNumber"],
                PostalCode = Request.Form["PostalCode"],
                City = Request.Form["City"],
                Country = Request.Form["Country"],
            };

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PutAsJsonAsync($"Customers/{CurrentCustomer.Id}", customer);
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("Index");
        }
    }
}
