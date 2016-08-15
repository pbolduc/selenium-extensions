namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public static class KendoExtensions
    {
        public static DatePicker KendoDatePicker(this IWebDriver webDriver, By by)
        {
            return new DatePicker(webDriver, by);
        }
    }
}