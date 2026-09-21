using Microsoft.AspNetCore.Mvc;

namespace TransportationManagementSystem.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
