using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie1_UI.Models;

namespace Zadanie1_UI.Pages
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Customer CurrentCustomer { get; set; } = new Customer();

        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
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
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.DeleteAsync($"Customers/{CurrentCustomer.Id}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("Index");
        }
    }
}
