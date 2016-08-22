namespace SeleniumExtensions.Tests.Kendo
{
    using System;
    using System.Threading;
    using OpenQA.Selenium;
    using Selenium.Extensions;
    using Selenium.Kendo;
    using Xunit;
    using Xunit.Abstractions;

    public class SliderTests : WidgetTests
    {
        public SliderTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Theory(DisplayName = "ComboBox - Basic")]
        [InlineData(WebDriverType.ChromeDriver)]
        public void Basic(WebDriverType driverType)
        {
            TestSettings.TestUri = new Uri("http://demos.telerik.com/kendo-ui/slider/index");
            TestSettings.DriverType = driverType;

            Driver = WebDriverManager.InitializeInstalledBrowserDriver(TestSettings, TestOutputHelper);
            Driver.Navigate().GoToUrl(TestSettings.TestUri);

            Driver.WaitForPageToLoad();

            var slider = new Slider(Driver, By.Id("slider"));

            var min = slider.Min;
            var max = slider.Max;
            var value = slider.Value;
            var showButtons = slider.ShowButtons;

            Assert.Equal(-10, min);
            Assert.Equal(10, max);
            Assert.Equal(0, value);
            Assert.Equal(true, showButtons);

            slider.Increment();

            value = slider.Value;
            Assert.Equal(2, value);



            Thread.Sleep(30*1000);
        }

    }
}