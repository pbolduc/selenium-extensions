namespace SeleniumExtensions.Tests.Kendo
{
    using System;
    using OpenQA.Selenium;
    using Selenium.Extensions;
    using Selenium.Extensions.Interfaces;
    using Selenium.Kendo;
    using Xunit;
    using Xunit.Abstractions;

    public class ComboBoxTests : WidgetTests
    {
        public ComboBoxTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Theory(DisplayName = "ComboBox - Basic")]
        [InlineData(WebDriverType.ChromeDriver)]
        public void Basic(WebDriverType driverType)
        {
            TestSettings.TestUri = new Uri("http://demos.telerik.com/kendo-ui/combobox/index");
            TestSettings.DriverType = driverType;

            Driver = WebDriverManager.InitializeInstalledBrowserDriver(TestSettings, TestOutputHelper);
            Driver.Navigate().GoToUrl(TestSettings.TestUri);

            //Driver.WaitForPageToLoad();

            var fabric = new ComboBox(Driver, By.Id("fabric"));
            var dataSource = fabric.DataSource;
            var textField = fabric.DataTextField;
            var valueField = fabric.DataValueField;

            Assert.NotNull(dataSource);
            Assert.Equal("text", textField);
            Assert.Equal("value", valueField);

            var size = new ComboBox(Driver, By.Id("size"));
            dataSource = size.DataSource;
            textField = size.DataTextField;
            valueField = size.DataValueField;
            Assert.NotNull(dataSource);
            Assert.Equal("text", textField);
            Assert.Equal("value", valueField);
        }
    }
}