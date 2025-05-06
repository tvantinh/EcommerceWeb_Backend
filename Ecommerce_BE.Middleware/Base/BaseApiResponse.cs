using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Ecommerce_BE.Middleware.Base
{
    public class BaseApiResponse(string message, object data, bool success, int status, Validation validations, string path)
    {
        public string message { get; set; } = message;
        public object data { get; set; } = data;
        public bool success { get; set; } = success;
        public int status { get; set; } = status;
        public Validation validations { get; set; } = validations;
        public string path { get; set; } = path;
        public int timestamp { get; set; } = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
    public class Validation(string fieldName, string message)
    {
        public string fieldName { get; set; } = fieldName;
        public string message { get; set; } = message;
    }
}
