namespace Selenium.Kendo
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public class Grid : Widget
    {
        public Grid(IWebDriver webDriver, By by)
            : base(webDriver, by, "kendoGrid")
        {
        }

        public void SelectRow(int row)
        {
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row), row, "row must be greater or equal to zero.");

            // see http://demos.telerik.com/kendo-ui/grid/api
            ExecuteJavaScript($"var row = {Target}.tbody.find('>tr:not(.k-grouping-row)').eq({row}); {Target}.select(row);");
        }

        public void ClearSelection()
        {
            ExecuteJavaScript($"{Target}.clearSelection();");
        }
    }
}