using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Orders.API.Controllers
{
    public class HomeController : Controller
    {
    

        public IActionResult Index()
        {
            return Redirect("/swagger");
        }


    }
}
