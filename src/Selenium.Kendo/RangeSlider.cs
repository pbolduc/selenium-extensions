namespace Selenium.Kendo
{
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;

    public class RangeSlider : Widget
    {
        public RangeSlider(ITestWebDriver driver, By by)
            : base(driver, by, "kendoRangeSlider")
        {
        }
    }
}