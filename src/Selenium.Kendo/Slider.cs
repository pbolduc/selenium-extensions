namespace Selenium.Kendo
{
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;

    public class Slider : Widget
    {
        public Slider(ITestWebDriver driver, By by)
            : base(driver, by, "kendoSlider")
        {
        }

        public long Min => (long)Driver.ExecuteScript(Scripts.Slider_options_min_get, FindElement());
        public long Max => (long)Driver.ExecuteScript(Scripts.Slider_options_max_get, FindElement());

        public long Value => (long)Driver.ExecuteScript(Scripts.Slider_value_get, FindElement());
        public bool ShowButtons => (bool)Driver.ExecuteScript(Scripts.Slider_options_showButtons_get, FindElement());

        public void Enable()
        {
            Driver.ExecuteScript(Scripts.Slider_enable, FindElement());
        }

        public void Disable()
        {
            Driver.ExecuteScript(Scripts.Slider_disable, FindElement());
        }

        public void Increment()
        {
            // find the element with k-button-increase class 
            Click("k-button-increase");
        }

        public void Decrease()
        {
            // find the element with k-button-decrease class 
            Click("k-button-decrease");
        }

        private void Click(string @class)
        {
            var button = FindElement().Parent().FindElement(By.ClassName(@class));
            button?.Click();
        }
}
}