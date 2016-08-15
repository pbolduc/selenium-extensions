namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public class ComboBox : Widget
    {
        public ComboBox(IWebDriver webDriver, By by)
            : base(webDriver, by, "kendoComboBox")
        {
        }
    }
}