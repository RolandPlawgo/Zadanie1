using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using Zadanie1_UI.Models;

namespace Zadanie1_UI.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Customer CurrentCustomer { get; set; } = new Customer();
        [BindProperty]
        public List<string>? ErrorMessages { get; set; } = null;

        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync(int id, List<string>? errorMessages = null)
        {
            ErrorMessages = errorMessages;

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
                ZipCode = Request.Form["ZipCode"],
                City = Request.Form["City"],
                Country = Request.Form["Country"],
            };

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PutAsJsonAsync($"Customers/{CurrentCustomer.Id}", customer);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                ErrorMessages = new List<string>();
                foreach (string message in result.errors.NIP)
                {
                    ErrorMessages.Add(message);
                }
                return RedirectToPage(new { id = CurrentCustomer.Id, errorMessages = ErrorMessages });
            }
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("Index");
        }
    }
}
