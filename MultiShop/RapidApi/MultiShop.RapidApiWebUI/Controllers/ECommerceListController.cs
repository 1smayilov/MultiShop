using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class ECommerceListController : Controller
    {
        public async Task<IActionResult> ECommerceList()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-product-search.p.rapidapi.com/search?q=logitech%20fare&country=tr&language=en&page=1&limit=10&sort_by=BEST_MATCH&product_condition=ANY&min_rating=ANY"),
                Headers =
        {
            { "x-rapidapi-key", "60ad7008c8msh9645191c6fd9e2ep15302bjsneab729fe50c0" },
            { "x-rapidapi-host", "real-time-product-search.p.rapidapi.com" },
        },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<ECommerceViewModel.Rootobject>(body);

                var products = values?.data?.products?.ToList() ?? new List<ECommerceViewModel.Product>();

                return View(products);
            }
        }

    }
}
