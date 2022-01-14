using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Models
{
    /// <summary>
    /// Bu sınıfın validayonu için fluent api validation kütüphanesi kullanacağız.
    /// </summary>
    public class CategoryUpdateInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
