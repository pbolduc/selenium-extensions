namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public static class WebElementExtensions
    {
        public static IWebElement Parent(this IWebElement e)
        {
            return e.FindElement(By.XPath(".."));
        }

        public static IWebElement NextSibling(this IWebElement e)
        {
            return e.NextSibling("*");
        }

        public static IWebElement NextSibling(this IWebElement e, string xpath)
        {
            return e.FindElement(By.XPath($"following-sibling::{xpath}"));
        }

        public static IWebElement PreviousSibling(this IWebElement e)
        {
            return e.PreviousSibling("*");
        }

        public static IWebElement PreviousSibling(this IWebElement e, string xpath)
        {
            return e.FindElement(By.XPath($"preceding-sibling::{xpath}"));
        }
    }
}