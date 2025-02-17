using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class DefaultController : Controller
    {
        public async Task<IActionResult> WeatherDetail()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://weatherly1.p.rapidapi.com/weather?city=Baku"),
                Headers =
    {
        { "x-rapidapi-key", "60ad7008c8msh9645191c6fd9e2ep15302bjsneab729fe50c0" },
        { "x-rapidapi-host", "weatherly1.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<WeatherViewModel>(body);
                ViewBag.cityTemp = values.temperature/40;
                return View();
            }
        }

        public async Task<IActionResult> Exchange()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/currency-exchange-rate?from_symbol=USD&to_symbol=AZN&language=en"),
                Headers =
    {
        { "x-rapidapi-key", "60ad7008c8msh9645191c6fd9e2ep15302bjsneab729fe50c0" },
        { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ExchangeModel.Rootobject>(body);
                ViewBag.exchange = values.data.exchange_rate;

                return View(); 
            }
        }


    }
}
