namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public class RangeSlider : Widget
    {
        public RangeSlider(IWebDriver webDriver, By by)
            : base(webDriver, by, "kendoRangeSlider")
        {
        }
    }
}