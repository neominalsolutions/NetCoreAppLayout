using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.TagHelpers
{
    public static class BsColorClass
    {
        public const string Success = "success";
        public const string Danger = "danger";
        public const string Info = "info";
        public const string Warning = "warning";
    }

    public static class BSElementType
    {
        public const string Alert = "alert";
        public const string Button = "btn";

    }

    // <bs-alert color="BsColorClass.Danger" message="Dikkat!" />

    [HtmlTargetElement("bs-alert", Attributes = "message, color")]
    public class AlertTagHelper:TagHelper
    {
        public string Message { get; set; } // attributeden gelen değerler ise property'e bağlanır.
        public string Color { get; set; }

        // yani tag helper view'den çağırdığımızda çalışan tetiklen method.
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div"; // çıktıyı div
            output.Attributes.Add("class", $"{BSElementType.Alert} {BSElementType.Alert}-{Color}");
            output.Attributes.Add("role", BSElementType.Alert);
            output.Content.SetContent(Message); //  A simple primary alert—check it out!

            //<div class="alert alert-primary" role="alert">
            //A simple primary alert—check it out!
            //</div >

            base.Process(context, output);
        }

    }
}
