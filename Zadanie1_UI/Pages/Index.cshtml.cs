using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie1_UI.Models;

namespace Zadanie1_UI.Pages
{
	public class IndexModel : PageModel
	{
		[BindProperty]
		public List<Customer> Customers { get; set; } = new List<Customer>();

		private readonly IHttpClientFactory _httpClientFactory;

		public IndexModel(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

        public async Task<IActionResult> OnGetAsync()
        {
			var client = _httpClientFactory.CreateClient("api");

			var result = await client.GetFromJsonAsync<List<Customer>?>("Customers");
			if (result is null)
			{
				return RedirectToPage("Error");
			}

			Customers = result;
			return Page();
        }
    }
}