namespace SeleniumExtensions.Tests.Kendo
{
    using System;
    using System.Threading;
    using OpenQA.Selenium;
    using Selenium.Extensions;
    using Selenium.Kendo;
    using Xunit;
    using Xunit.Abstractions;

    public class DatePickerTests : WidgetTests
    {
        public DatePickerTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            TestSettings.TestUri = new Uri("http://selenium-extensions.azurewebsites.net/kendo/{version}/examples/datepicker/index.html");
            TestSettings.LogLevel = Selenium.Extensions.LogLevel.Basic;
            TestSettings.TestDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tests\\";
            TestSettings.UseNativeDriver = true;
        }

        [Theory(DisplayName = "DatePicker - Basic")]
        [InlineData(WebDriverType.ChromeDriver, "2016.2.714")]

        public void Basic(WebDriverType driverType, string version)
        {
            SetVersion(version);

            TestSettings.DriverType = driverType;
            Driver = WebDriverManager.InitializeInstalledBrowserDriver(TestSettings, TestOutputHelper);
            Driver.Navigate().GoToUrl(TestSettings.TestUri);

            DatePicker datePicker = new DatePicker(Driver, By.CssSelector("input#datepicker"));


            datePicker.ClickCalendarIcon();
            Thread.Sleep(200 * 3 / 2);

            var calendar = datePicker.GetCalendar();

            calendar.ClickDate(new DateTime(1970, 6, 12));
            Thread.Sleep(1000);

            for (int i = 0; i < 1; i++)
            {
                datePicker.ClickCalendarIcon();
                Thread.Sleep(1000);
                datePicker.ClickCalendarIcon();
                Thread.Sleep(1000);
            }

            Driver.Close();
        }

        private void SetVersion(string version)
        {
            var uri = TestSettings.TestUri.ToString().Replace("/{version}/", "/" + version + "/");
            TestSettings.TestUri = new Uri(uri);
        }
    }
}