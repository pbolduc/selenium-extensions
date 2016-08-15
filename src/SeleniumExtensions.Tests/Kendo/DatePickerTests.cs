namespace SeleniumExtensions.Tests.Kendo
{
    using System;
    using System.Threading;
    using OpenQA.Selenium;
    using Selenium.Kendo;
    using SeleniumExtensions.Tests.Support;
    using Xunit;
    using Xunit.Abstractions;

    public class DatePickerTests : IDisposable
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly TestSettings _testSettings;
        private IWebDriver _driver;

        public DatePickerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            _testSettings = TestSettings.Default();
            _testSettings.TestUri = new Uri("http://selenium-extensions.azurewebsites.net/kendo/{version}/DatePicker/basic.html");
        }

        [Theory(DisplayName = "DatePicker - Basic")]
        [InlineData(WebDriverType.ChromeDriver, "2016.2.714")]

        public void Basic(WebDriverType driverType, string version)
        {
            SetVersion(version);

            _driver = WebDriverManager.InitializeWebDriver(_testSettings, _testOutputHelper);
            _driver.Navigate().GoToUrl(_testSettings.TestUri);

            DatePicker datePicker = _driver.KendoDatePicker(By.Id("datepicker"));

            var max = datePicker.Max;

            datePicker.Max = new DateTime(2017,12,31, 8, 0, 0, DateTimeKind.Utc);
            max = datePicker.Max;

            datePicker.ClickCalendarIcon();
            Thread.Sleep(200 * 3 / 2);
            
            var calendar = datePicker.GetCalendar();

            calendar.ClickDate(new DateTime(1970, 6, 12));
            Thread.Sleep(10000);

            //calendar.Foo((year, month, day) =>
            //{
            //    _testOutputHelper.WriteLine($"year/month/day {year}/{month}/{day}");
            //});

            for (int i = 0; i < 1; i++)
            {
                datePicker.ClickCalendarIcon();
                Thread.Sleep(1000);
                datePicker.ClickCalendarIcon();
                Thread.Sleep(1000);
            }
            
            _driver.Close();
        }

        private void SetVersion(string version)
        {
            var uri = _testSettings.TestUri.ToString().Replace("/{version}/", "/" + version + "/");
            _testSettings.TestUri = new Uri(uri);
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _driver?.Quit();
            }
        }
    }
}
