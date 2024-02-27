using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net;
using Zadanie1_UI.Models;

namespace Zadanie1_UI.Pages
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Customer? CurrentCustomer { get; set; } = null;
        [BindProperty]
        public List<string>? ErrorMessages { get; set; } = null;

        private readonly IHttpClientFactory _httpClientFactory;

        public AddModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet(List<string>? errorMessages = null)
        {
            if (TempData["Customer"]?.ToString() is not null)
            {
                if (TempData["Customer"]!.ToString() != "")
                {
                    CurrentCustomer = JsonConvert.DeserializeObject<Customer>(TempData["Customer"].ToString());
                }
            }
            ErrorMessages = errorMessages;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Customer? customer = new Customer();
            customer.Name = Request.Form["CustomerName"];
            customer.NIP = Request.Form["NIP"];
            customer.Address = new Address()
            {
                Street = Request.Form["Street"],
                HouseNumber = Request.Form["HouseNumber"],
                ZipCode = Request.Form["ZipCode"],
                City = Request.Form["City"],
                Country = Request.Form["Country"],
            };

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync($"Customers", customer);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var test = await response.Content.ReadAsStringAsync();

                var result = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                ErrorMessages = new List<string>();
                if (result?.errors.Name is not null)
                {
                    foreach (string message in result.errors.Name)
                    {
                        ErrorMessages.Add(message);
                    }
                }
                else if (result?.errors.NIP is not null)
                {
                    foreach (string message in result.errors.NIP)
                    {
                        ErrorMessages.Add(message);
                    }
                }
                else
                {
                    ErrorMessages.Add("All Address fields are required");
                }

                TempData["Customer"] = JsonConvert.SerializeObject(customer);
                return RedirectToPage(new { customer = customer, errorMessages = ErrorMessages });
            }
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("Index");
        }
    }
}
