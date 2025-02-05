using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Ödəmə Ekranı";
            ViewBag.directory3 = "Kartla Ödəmə";
            return View();
        }
    }
}
