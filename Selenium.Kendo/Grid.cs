namespace Selenium.Kendo
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public class Grid : Widget
    {
        public Grid(By by)
            : base(by, "kendoGrid")
        {
        }

        public void SelectRow(IWebDriver driver, int row)
        {
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row), row, "row must be greater or equal to zero.");

            // see http://demos.telerik.com/kendo-ui/grid/api
            var js = $"var row = {Target}.tbody.find('>tr:not(.k-grouping-row)').eq({row}); {Target}.select(row);";
            ExecuteJavaScript(driver, js);
        }

        public void ClearSelection(IWebDriver driver)
        {
            var js = $"{Target}.clearSelection();";
            ExecuteJavaScript(driver, js);
        }
    }
}