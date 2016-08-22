namespace Selenium.Kendo
{
    using System;
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;

    public abstract class Widget
    {
        private const string InitialClassName = "_kendo_widget_";
        private static string _pageObjectName = InitialClassName + Guid.NewGuid().ToString("n");
        private string _thisUuid = Guid.NewGuid().ToString("n");

        protected By By { get; }
        protected string Name { get; }
        protected ITestWebDriver Driver { get; }

        private IWebElement _element;

        protected Widget(ITestWebDriver driver, By by, string name)
        {
            Driver = driver;
            By = by;
            Name = name;
        }

        protected DateTime BrowserNow()
        {
            var milliseconds = (long)Driver.ExecuteScript("return Date.now()");
            return KendoDate.Epoch.AddMilliseconds(milliseconds);
        }

        /// <summary>
        /// Finds the current element.
        /// </summary>
        /// <returns></returns>
        protected IWebElement FindElement(bool force = false)
        {
            if (_element == null || force)
            {
                _element = Driver.FindElement(By);
            }

            return _element;
        }
    }
}
