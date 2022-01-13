using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // IActionResult ile ActionResult aynı resultları döndürebilir fakat IActionResult interface olarak işaretlenmiştir. Net core da 
        public IActionResult About()
        {
            return View();
        }
    }
}
