namespace SeleniumExtensions.Tests.Kendo
{
    using System;
    using OpenQA.Selenium;
    using Selenium.Extensions;
    using Selenium.Kendo;
    using Xunit;
    using Xunit.Abstractions;

    public class DropDownListTests : WidgetTests
    {
        public DropDownListTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Theory(DisplayName = "ComboBox - Basic")]
        [InlineData(WebDriverType.ChromeDriver)]
        public void Basic(WebDriverType driverType)
        {
            TestSettings.TestUri = new Uri("http://demos.telerik.com/kendo-ui/dropdownlist/index");
            TestSettings.DriverType = driverType;

            Driver = WebDriverManager.InitializeInstalledBrowserDriver(TestSettings, TestOutputHelper);
            Driver.Navigate().GoToUrl(TestSettings.TestUri);

            Driver.WaitForPageToLoad();

            var color = new DropDownList(Driver, By.Id("color"));
            var dataSource = color.DataSource;
            var textField = color.DataTextField;
            var valueField = color.DataValueField;

            Assert.NotNull(dataSource);
            Assert.Equal("text", textField);
            Assert.Equal("value", valueField);
        }
    }
}