namespace Selenium.Kendo
{
    using System;
    using System.Collections.ObjectModel;
    using OpenQA.Selenium;



    public class DatePicker : Widget
    {
        public IDatePickerApi Api { get; }

        public DatePicker(IWebDriver webDriver, By by)
            : base(webDriver, by, "kendoDatePicker")
        {
            Api = new DatePickerApi(this);
        }

        public Calendar GetCalendar()
        {
            var div = ExecuteJavaScript<ReadOnlyCollection<IWebElement>>($"return {Target}.dateView.div;");
            
            var calendarContainer = div[0];
            var calendarElement = calendarContainer.FindElement(By.XPath(".//div[@class='k-widget k-calendar']"));

            Calendar calendar = new Calendar(WebDriver, By.Id(calendarElement.GetAttribute("id")));
            return calendar;
        }

        public void ClickCalendarIcon()
        {
            var element = Element.NextSibling("span[@class='k-select']");
            element.Click();
        }

        public virtual void SetValue(DateTime value, string format)
        {
            string stringValue = FormatDate(value, format);
            SetValue(stringValue);
        }

        public bool PopupCalendarVisible => ExecuteJavaScript<bool>($"return {Target}.dateView.popup.visible();");

        public string Format => ExecuteJavaScript<string>($"return {Target}.options.format;");
        public string Depth => ExecuteJavaScript<string>($"return {Target}.options.depth;");
        public string Start => ExecuteJavaScript<string>($"return {Target}.options.start;");
        public string Footer => ExecuteJavaScript<string>($"return {Target}.options.footer;");
        public string Prefix => ExecuteJavaScript<string>($"return {Target}.options.prefix;");

        public DateTime Min
        {
            get { return AsDate($"return {Target}.min().toISOString();"); }
            set
            {
                var iso = value.ToString("O");
                ExecuteJavaScript($"var min = new Date('{iso}');{Target}.min(min);");
            }
        }

        public DateTime Max
        {
            get { return AsDate($"return {Target}.max().toISOString();"); }
            set
            {
                var iso = value.ToString("O");
                ExecuteJavaScript($"var max = new Date('{iso}');{Target}.max(max);");
            }
        }

        private void JavaScriptOpen()
        {
            ExecuteJavaScript($"{Target}.open();");
        }

        private void JavaScriptClose()
        {
            ExecuteJavaScript($"{Target}.close();");
        }

        private string FormatDate(DateTime value, string format)
        {
            var formatted = value.ToString(format);
            return formatted;
        }

        private string FormatDate(DateTime value, string format, IFormatProvider provider)
        {
            var formatted = value.ToString(format, provider);
            return formatted;
        }

        public interface IDatePickerApi : IKendoWidgetApi
        {
            /// <summary>
            /// Uses JavaScript API to open the calendar.
            /// </summary>
            void Open();
            /// <summary>
            /// Uses JavaScript API to close the calendar.
            /// </summary>
            void Close();
        }

        private class DatePickerApi : KendoWidgetApi<DatePicker>, IDatePickerApi
        {

            public DatePickerApi(DatePicker datePicker)
                : base(datePicker)
            {
            }

            public void Open()
            {
                Widget.JavaScriptOpen();
            }

            public void Close()
            {
                Widget.JavaScriptClose();
            }
        }
    }
}