namespace Selenium.Kendo
{
    using System;
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;

    public class DropDownList : Widget
    {
        public DropDownList(ITestWebDriver driver, By by)
            : base(driver, by, "kendoDropDownList")
        {
        }

        public string DataSource => (string)Driver.ExecuteScript(Scripts.DropDownList_datasource_data, FindElement());
        public string DataTextField => (string)Driver.ExecuteScript(Scripts.DropDownList_options_dataTextField, FindElement());
        public string DataValueField => (string)Driver.ExecuteScript(Scripts.DropDownList_options_dataValueField, FindElement());
    }
}