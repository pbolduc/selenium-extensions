namespace SeleniumExtensions.Tests.Support
{
    using System.ComponentModel;

    [Description("Driver")]
    public enum WebDriverType
    {
        [Description("Chrome")] ChromeDriver,
        [Description("Firefox")] FirefoxDriver,
        [Description("IE")] InternetExplorerDriver,
        [Description("Edge")] EdgeDriver,
        [Description("Opera")] OperaDriver,
        [Description("Remote")] RemoteWebDriver,
        [Description("Safari")] SafariDriver,
        [Description("Emulator")]EmulatorDriver,
    }
}
