using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Middleware.Base
{
    public class BaseApiException : Exception
    {
        public int StatusCode { get; set; }

        public IEnumerable<ValidationError>? Errors { get; set; }

        public string? ReferenceErrorCode { get; set; }
        public string? ReferenceDocumentLink { get; set; }

        public BaseApiException(string message, int statusCode = 500, IEnumerable<ValidationError>? errors = null, string errorCode = "", string refLink = "") : base(message)
        {
            this.StatusCode = statusCode;
            this.Errors = errors;
            this.ReferenceErrorCode = errorCode;
            this.ReferenceDocumentLink = refLink;
        }

        public BaseApiException(System.Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
