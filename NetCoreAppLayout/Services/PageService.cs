using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Services
{
    public class PageService
    {
        public string PageResult(string title, string body)
        {
            string titleTemplate = $"<h1>{title}</h1>";
            string bodyTemplate = $"<p>{body}</p>";






            // String birleştirme işlemleri için StringBuilder kullandık.
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(titleTemplate);
            stringBuilder.AppendLine(bodyTemplate);




            return stringBuilder.ToString();
        }
    }
}
