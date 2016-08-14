namespace Selenium.Kendo
{
    using System;
    using OpenQA.Selenium;

    public class DatePicker : SimpleWidget
    {
        public DatePicker(By by) 
            : base(by, "kendoDatePicker")
        {
        }

        public void Open(IWebDriver driver)
        {
            ExecuteJavaScript(driver, $"{Target}.open();");
        }

        public void Close(IWebDriver driver)
        {
            ExecuteJavaScript(driver, $"{Target}.close();");
        }

        public virtual void SetValue(IWebDriver driver, DateTime value, string format)
        {
            string stringValue = Format(value, format);
            SetValue(driver, stringValue);
        }

        private string Format(DateTime value, string format)
        {
            var formatted = value.ToString(format);
            return formatted;
        }

        private string Format(DateTime value, string format, IFormatProvider provider)
        {
            var formatted = value.ToString(format, provider);
            return formatted;
        }
    }
}