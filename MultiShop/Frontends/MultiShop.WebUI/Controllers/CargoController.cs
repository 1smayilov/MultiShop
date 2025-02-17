using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    public class CargoController : Controller
    {
        public IActionResult CargoList()
        {
            return View();
        }
    }
}
