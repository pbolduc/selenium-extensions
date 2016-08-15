namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public class DropDownList : Widget
    {
        public DropDownList(IWebDriver webDriver, By by)
            : base(webDriver, by, "kendoDropDownList")
        {
        }
    }
}