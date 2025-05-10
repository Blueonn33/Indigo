using Microsoft.AspNetCore.Mvc;

namespace Indigo.Controllers
{
    public class DocumentationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
