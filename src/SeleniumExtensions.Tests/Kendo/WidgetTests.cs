namespace SeleniumExtensions.Tests.Kendo
{
    using System;
    using Selenium.Extensions;
    using Selenium.Extensions.Interfaces;
    using Xunit.Abstractions;

    public abstract class WidgetTests : IDisposable
    {
        protected ITestWebDriver Driver { get; set; }
        protected TestSettings TestSettings { get; }
        protected ITestOutputHelper TestOutputHelper { get; }

        protected WidgetTests(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;

            TestSettings = TestSettings.Default;
            TestSettings.LogLevel = Selenium.Extensions.LogLevel.Basic;
            TestSettings.TestDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Tests\\";
            TestSettings.UseNativeDriver = true;
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
                Driver?.Quit();
            }
        }
    }
}