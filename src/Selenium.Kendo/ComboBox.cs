namespace Selenium.Kendo
{
    using OpenQA.Selenium;

    public class ComboBox : SimpleWidget
    {
        public ComboBox(By by)
            : base(by, "kendoComboBox")
        {
        }
    }
}