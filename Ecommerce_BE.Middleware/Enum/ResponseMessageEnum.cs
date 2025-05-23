using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Middleware.Enum
{
    public enum ResponseMessageEnum
    {
        [Description("Request successful.")]
        Success = 0 ,
        [Description("Request responded with exceptions.")]
        Exception = 1,
        [Description("Request denied.")]
        UnAuthorized = 2,
        [Description("Request responded with validation error(s).")]
        ValidationError = 3,
        [Description("Unable to process the request.")]
        Failure = 4
    }
    public static class ResponseMessageEnumExtensions
    {
        public static string GetDescription(this ResponseMessageEnum value)
        {
            Type enumType = value.GetType();
            FieldInfo? fieldInfo = enumType.GetField(value.ToString());

            if (fieldInfo == null)
            {
                return value.ToString();
            }

            DescriptionAttribute? descriptionAttribute = fieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }
    }
}
