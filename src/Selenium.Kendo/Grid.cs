namespace Selenium.Kendo
{
    using System;
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;

    public class Grid : Widget
    {
        public Grid(ITestWebDriver driver, By by)
            : base(driver, by, "kendoGrid")
        {
        }

        public void SelectRow(int row)
        {
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row), row, "row must be greater or equal to zero.");
            Driver.ExecuteScript(Scripts.Grid_select_row, FindElement(), row);
        }

        public void ClearSelection()
        {
            Driver.ExecuteScript(Scripts.Grid_clearSelection, FindElement());
        }
    }
}