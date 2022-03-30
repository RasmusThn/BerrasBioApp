using Microsoft.AspNetCore.Mvc;


namespace BerrasBio.Controllers
{
    public class HomeController : Controller
    {     
        public IActionResult Index()
        {
            return View();
        }
       
    }
}