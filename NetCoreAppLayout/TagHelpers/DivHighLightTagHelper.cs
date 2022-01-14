using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.TagHelpers
{
    // div elementine color ile gönderilen renege göre div elementinin rengini değiştiriyoruz.
    // div elementine uygulanabilir bir attribute olmuş oldu
    [HtmlTargetElement("div",Attributes ="color")]
    public class DivHighLightTagHelper: TagHelper
    {
        public string Color { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("class", $"text-{Color}");
            base.Process(context, output);
        }
    }
}
