using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _ContactDetailComponentPartial  : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
