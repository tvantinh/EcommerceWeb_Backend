using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ecommerce_BE.Middleware.Base
{
    public class BaseApiError
    {
        public bool ? IsError { get; } = true;
        public string  ExceptionMessage { get; }
        public string ? Details { get; set; }
        public string ? ReferenceErrorCode { get; set; }
        public string ? ReferenceDocumentLink { get; set; }
        public IEnumerable<ValidationError> ? ValidationErrors { get; set; }

        public BaseApiError(string message)
        {
            ExceptionMessage = message;
        }

        public BaseApiError(ModelStateDictionary modelState)
        {
            if (modelState != null && modelState.Any(kvp => kvp.Value.Errors.Count > 0))
            {
                ExceptionMessage = "Please correct the specified validation errors and try again.";
                ValidationErrors = modelState
                    .Where(kvp => kvp.Value.Errors.Count > 0)
                    .SelectMany(kvp => kvp.Value.Errors.Select(error => new ValidationError(kvp.Key, error.ErrorMessage)))
                    .ToList();
            }
            else
            {
                ExceptionMessage = "An unknown error occurred.";
                ValidationErrors = new List<ValidationError>();
            }
        }
    }

    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ? Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = string.IsNullOrWhiteSpace(field) ? null : field;
            Message = message;
        }
    }
}
