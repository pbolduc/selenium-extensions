namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public class DropDownList : SimpleWidget
    {
        public DropDownList(By by)
            : base(by, "kendoDropDownList")
        {
        }
    }
}