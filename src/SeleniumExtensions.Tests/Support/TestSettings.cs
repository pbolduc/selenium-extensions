namespace SeleniumExtensions.Tests.Support
{
    using System;
    using System.IO;
    using System.Reflection;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using Xunit.Abstractions;

    public class TestSettings
    {
        public WebDriverType DriverType { get; set; }

        public TimeSpan Timeout { get; set; }

        public Uri TestUri { get; set; }

        public static TestSettings Default()
        {
            var settings = new TestSettings();

            settings.DriverType = WebDriverType.ChromeDriver;
            settings.Timeout = TimeSpan.FromSeconds(30);

            return settings;
        }
    }

    public class WebDriverManager
    {
        public static IWebDriver WebDriver;
        public static ITestOutputHelper TestOutputHelper;

        public static IWebDriver InitializeWebDriver(TestSettings testSettings, ITestOutputHelper testOutputHelper)
        {
            switch (testSettings.DriverType)
            {
                case WebDriverType.ChromeDriver:
                    return CreateChromeDriver(testSettings);
                default:
                    throw new NotImplementedException();
            }
        }

        private static IWebDriver CreateChromeDriver(TestSettings testSettings)
        {
            string driverLocation = Path.Combine(AssemblyDirectory, "chromedriver.exe");
            string driverPath = Path.GetDirectoryName(driverLocation);
            string driverExecutable = Path.GetFileName(driverLocation);
            var driverService = ChromeDriverService.CreateDefaultService(driverPath, driverExecutable);

            var options = new ChromeOptions();
            options.LeaveBrowserRunning = false;
            options.AddArgument("--no-default-browser-check");
            options.AddArgument("--test-type=browser");
            options.AddArgument("--start-maximized");
            options.AddArgument("--allow-no-sandbox-job");
            options.AddArgument("--disable-component-update");
            var driver = new ChromeDriver(driverService, options, testSettings.Timeout);

            return driver;
        }

        private static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}