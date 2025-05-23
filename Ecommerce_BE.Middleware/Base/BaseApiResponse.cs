using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Ecommerce_BE.Middleware.Base
{
    public class BaseApiResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
        public string Path { get; set; }
        public int Timestamp { get; set; }

        public BaseApiResponse(string message, object data, bool success, int status, string path)
        {
            Message = message;
            Data = data;
            Success = success;
            Status = status;
            Path = path;
            Timestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

    }
}

