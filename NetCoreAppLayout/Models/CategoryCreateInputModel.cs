using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Models
{
    // Input model son kullanıcı end user tarafından viewdan doldurup gönderilen ve arka planda entity'e değerleri maplenen sınıflardır. Amacı doğrulanmış form bilgilerini içerisinde tutmaktır. Bu sebeple sadece propertylerden oluşur. Veri taşıma görevi görür. Ayrıcı bu model üzerinde validayon (veri doğrulama) operasyonları yapılır.Verileri veri tabanına göndermen önce entitylere maplemeden önce bu katmanı kullanırız. Validasyon işlemleri için DataAnnotations kullanırız. Daha kompleks yapılarda fluent validation da tercih edilebilir.
    public class CategoryCreateInputModel
    {
        [Required(ErrorMessage ="Name alanı boş geçilemez")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage ="En fazla 200 karakter uzunluğunda giriniz")]
        public string Description { get; set; }

    }
}
