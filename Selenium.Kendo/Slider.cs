namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public class Slider : Widget
    {
        public Slider(By by)
            : base(by, "kendoSlider")
        {
        }

        public void Enable(IWebDriver driver)
        {
            var js = $"{Target}.enable();";
            ExecuteJavaScript(driver, js);
        }

        public void Disable(IWebDriver driver)
        {
            var js = $"{Target}.disable();";
            ExecuteJavaScript(driver, js);
        }

        public void Increment(IWebDriver driver)
        {
            // find the element with k-button-increase class 
            Click(driver, "k-button-increase");
        }

        public void Decrease(IWebDriver driver)
        {
            // find the element with k-button-decrease class 
            Click(driver, "k-button-decrease");
        }

        private void Click(IWebDriver driver, string @class)
        {
            var button = ParentElement(driver).FindElement(By.ClassName(@class));
            button.Click();
        }
}
}