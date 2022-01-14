using Microsoft.AspNetCore.Mvc;
using NetCoreAppLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("create-category")] // Attribute routing yani sayfa buradaki route ile açılsın demek
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create-category")] // httppost olarak create-category ile sayfaya istek atılsın demek
        // [Bind("Name","Description")] attribute ile bazen formdaki bazı alanların sunucudaki methoda taşınmasını istemeyiz böyle durumlarda ilgili propertyNameleri yazmayız. Yazdıklarımız sunucuya post edilir.
        public IActionResult Create([Bind("Name","Description")] CategoryCreateInputModel model)
        {

            if (ModelState.IsValid)
            {
                // model validasyondan geçiyorsa demek.
                // veri tabanına kaydı gönder 
            }

            return View();
        }
    }
}
