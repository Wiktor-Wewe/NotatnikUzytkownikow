using Microsoft.AspNetCore.Mvc;

namespace NotatnikVeloce.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
