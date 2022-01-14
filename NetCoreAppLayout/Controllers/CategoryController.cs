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

        /*     [HttpGet("create-category")]*/ // Attribute routing yani sayfa buradaki route ile açılsın demek
        //[HttpGet]
        [HttpGet("create-category")]
        public IActionResult Create()
        {
            return View();
        }

        /*    [HttpPost("create-category-post")]*/ // httppost olarak create-category ile sayfaya istek atılsın demek
                                                   // [Bind("Name","Description")] attribute ile bazen formdaki bazı alanların sunucudaki methoda taşınmasını istemeyiz böyle durumlarda ilgili propertyNameleri yazmayız. Yazdıklarımız sunucuya post edilir.
        //[HttpPost]
        [HttpPost("create-category-post")]
        public IActionResult Create([Bind("Name","Description")] CategoryCreateInputModel model)
        {
            // bu alan post olduktan sonra yakalanır. Çünkü dataAnnotation ile hatayı bağlamadık. sadece jquery-validate ile sayfa post olmadan DataAnnotation yazılan hata mesajlarını ekranda görebiliriz.
            ModelState.AddModelError("Error", "Different Error");

            if (ModelState.IsValid)
            {
                // model validasyondan geçiyorsa demek.
                // veri tabanına kaydı gönder 
            }

            return View();
        }



        [HttpGet("update-category")]
        public IActionResult Update()
        {
            // form dolu gelsin
            var model = new CategoryUpdateInputModel
            {
                Name = "Deneme",
                Description = "Test"
            };

            return View(model);
        }


        [HttpPost("update-category-post")]
        public IActionResult Update([Bind("Name", "Description")] CategoryUpdateInputModel model)
        {
         
            ModelState.AddModelError("Error", "Different Error");

            if (ModelState.IsValid)
            {
                // model validasyondan geçiyorsa demek.
                // veri tabanına kaydı gönder 
            }

            return View();
        }
    }
}
