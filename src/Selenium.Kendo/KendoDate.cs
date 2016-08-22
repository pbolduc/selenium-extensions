using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Kendo
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public static class KendoDate
    {
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ParseDate(IWebDriver driver, string value, string format)
        {
            var o = (long)driver.ExecuteJavaScript<long>("return kendo.parseDate(arguments[0],arguments[1]).getTime()", value, format);
            return Epoch.AddMilliseconds(o);
        }

        public static string ToString(IWebDriver driver, DateTime value, string format)
        {
            int milliseconds = (int)value.Subtract(Epoch).TotalMilliseconds;
            var o = driver.ExecuteJavaScript<string>("var a = arguments;return kendo.toString(new Date(a[0]), a[1])",
                milliseconds,
                format);

            return o;
        }
    }
}
