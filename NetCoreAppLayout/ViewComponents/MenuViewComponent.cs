using Microsoft.AspNetCore.Mvc;
using NetCoreAppLayout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.ViewComponents
{
    public class MenuViewComponent: ViewComponent
    {
        // ViewComponent IViewComponentResult result döndürür. Temelde sayfa açılırken asenkron paralelde bir html sayfaya render etme işini görürür. bu sebeple performans için viewcomponentler async yazılmıştır.
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new List<MenuViewModel>
            {
                new MenuViewModel
                {
                    Id = 1,
                    Title = "Menu-1",
                    Url = "/Home/Menu1"
                },
                new MenuViewModel
                {
                    Id= 2,
                    Title = "Menu-2",
                    Url = "/Home/Menu2"
                }
            };

            return View(model);
        }

    }
}
