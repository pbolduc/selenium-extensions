namespace Selenium.Kendo
{
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;

    public static class KendoExtensions
    {
        public static DatePicker KendoDatePicker(this ITestWebDriver driver, By by)
        {
            return new DatePicker(driver, by);
        }
    }
}