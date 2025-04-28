using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Core.Utils
{
    public static class TimeHelper
    {
        public static DateTimeOffset ConvertToUTCPlus7(DateTimeOffset dateTimeOffset)
        {
            TimeSpan utcPlus7Offset = new TimeSpan(7, 0, 0);
            return dateTimeOffset.ToOffset(utcPlus7Offset);
        }

        public static DateTimeOffset ConvertToUTCPlus7NotChanges(DateTimeOffset dateTimeOffset)
        {
            TimeSpan utcPlus7Offset = new TimeSpan(7, 0, 0);
            return dateTimeOffset.ToOffset(utcPlus7Offset).AddHours(-7);
        }
    }
}
