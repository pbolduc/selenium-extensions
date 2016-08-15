namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public class Slider : Widget
    {
        public Slider(IWebDriver webDriver, By by)
            : base(webDriver, by, "kendoSlider")
        {
        }

        public void Enable()
        {
            var js = $"{Target}.enable();";
            ExecuteJavaScript(js);
        }

        public void Disable()
        {
            var js = $"{Target}.disable();";
            ExecuteJavaScript(js);
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
            var button = Element.Parent().FindElement(By.ClassName(@class));
            button?.Click();
        }
}
}