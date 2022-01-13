using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreAppLayout.Models
{
   
    public static class RequestContentType
    {
        public const string JSON = "application/json"; // bu değer artık set edilemez.
        public const string FormData = "application/x-www-form-urlencoded";
        public const string TextPlain = "text/plain; charset=utf-8";
        public const string Html = "text/html; charset=utf-8";
    }
}
