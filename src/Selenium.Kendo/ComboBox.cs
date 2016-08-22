namespace Selenium.Kendo
{
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;

    public class ComboBox : Widget
    {
        public ComboBox(ITestWebDriver driver, By by)
            : base(driver, by, "kendoComboBox")
        {
        }

        public string DataSource => (string)Driver.ExecuteScript(Scripts.ComboBox_datasource_data, FindElement());
        public string DataTextField => (string)Driver.ExecuteScript(Scripts.ComboBox_options_dataTextField, FindElement());
        public string DataValueField => (string)Driver.ExecuteScript(Scripts.ComboBox_options_dataValueField, FindElement());
    }
}